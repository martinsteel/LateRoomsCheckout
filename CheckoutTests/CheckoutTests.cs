using LateRoomsCheckout;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Mock<IProductRepository> productRepository;

        [SetUp]
        public void SetUp()
        {
            // Set up mock product data. This would probably be in a DB in production.
            productRepository = new Mock<IProductRepository>();
            productRepository.Setup(r => r.Get("A")).Returns(new Product { SKU = "A", UnitPrice = 50, SpecialPrice = 130, SpecialQuantity = 3 });
            productRepository.Setup(r => r.Get("B")).Returns(new Product { SKU = "B", UnitPrice = 30, SpecialPrice =  45, SpecialQuantity = 2 });
            productRepository.Setup(r => r.Get("C")).Returns(new Product { SKU = "C", UnitPrice = 20 });
            productRepository.Setup(r => r.Get("D")).Returns(new Product { SKU = "D", UnitPrice = 15 });
        }

        [Test]
        public void Can_Scan_Item()
        {
            var checkout = new Checkout(productRepository.Object);

            var scanResult = checkout.Scan("A");

            Assert.That(scanResult, Is.True);
        }

        [Test]
        public void Can_Scan_Multiple_Items()
        {
            var checkout = new Checkout(productRepository.Object);

            var scanResultA = checkout.Scan("A");
            var scanResultB = checkout.Scan("B");

            Assert.That(scanResultA, Is.True);
            Assert.That(scanResultB, Is.True);

        }

        [Test]
        [TestCase("A", 50)]
        [TestCase("D", 15)]
        public void Can_Get_Correct_Price_For_Single_Item(string sku, int expectedPrice)
        {
            var checkout = new Checkout(productRepository.Object);

            checkout.Scan(sku);
            var total = checkout.GetTotalPrice();

            Assert.That(total, Is.EqualTo(expectedPrice));
        }

        [Test]
        public void Scanning_Invalid_SKU_Fails()
        {
            var checkout = new Checkout(productRepository.Object);

            var scanResult = checkout.Scan("Z");

            Assert.That(scanResult, Is.False);
        }

        [Test]
        public void Checkout_Total_Is_Zero_When_Nothing_Scanned()
        {
            var checkout = new Checkout(productRepository.Object);

            var total = checkout.GetTotalPrice();

            Assert.That(total, Is.Zero);
        }

        [Test]
        public void Single_Product_Gets_Muilti_Buy_Discount()
        {
            var checkout = new Checkout(productRepository.Object);

            // Product A should scan at 3 for 130 rather than 150 (3 * 50)
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var total = checkout.GetTotalPrice();

            Assert.That(total, Is.EqualTo(130));
        }

        [Test]
        public void Full_Price_Used_If_Not_Buying_Exact_Multiple_Of_Special_Quantity()
        {
            var checkout = new Checkout(productRepository.Object);

            for (int i = 0; i < 7; i++)
            {
                checkout.Scan("B");
            }
            var total = checkout.GetTotalPrice();

            // Product B is 30 or 2 for 45, so 7 items = 165 (3 * 45 + 1 * 30)
            Assert.That(total, Is.EqualTo(165));
        }

        [Test]
        public void Discount_Applied_For_Mix_Of_Products()
        {
            var checkout = new Checkout(productRepository.Object);

            var items = new List<string> {"A", "A", "A", "A", "A", "B", "B", "B", "B", "C", "D", "D", "D", "D", "D", "D", "D"};

            // Randomly sort items to make sure implementation doesn't rely on order.
            var rng = new Random();
            var shuffledItems = items.OrderBy(x => rng.Next()).ToList();

            shuffledItems.ForEach(x => checkout.Scan(x));

            var total = checkout.GetTotalPrice();

            /* A (5): 1 * 130 + 2 * 50 (three discounted, 2 full price)
             * B (4): 2 * 45 (all four discounted)
             * C (1): 1 * 20
             * D (7): 7 * 15
             * Total: 445
             */
            Assert.That(total, Is.EqualTo(445));
        }
    }
}
