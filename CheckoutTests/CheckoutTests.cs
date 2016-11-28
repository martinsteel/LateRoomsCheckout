using LateRoomsCheckout;
using Moq;
using NUnit.Framework;
using System;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Mock<IProductRepository> productRepository;

        [SetUp]
        public void SetUp()
        {
            // Set up mock product data. This would probably be in a DB.
            productRepository = new Mock<IProductRepository>();
            productRepository.Setup(r => r.Get("A")).Returns(new Product { SKU = "A", UnitPrice = 50 });
            productRepository.Setup(r => r.Get("B")).Returns(new Product { SKU = "B", UnitPrice = 30 });


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
        public void Can_Get_Correct_Price_For_Single_Item()
        {
            var checkout = new Checkout(productRepository.Object);

            checkout.Scan("A");
            var total = checkout.GetTotalPrice();

            Assert.That(total, Is.EqualTo(50));
        }

        [Test]
        public void Scanning_Invalid_SKU_Fails()
        {
            var checkout = new Checkout(productRepository.Object);

            var scanResult = checkout.Scan("Z");
            Assert.That(scanResult, Is.False);
        }
    }
}
