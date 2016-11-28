using LateRoomsCheckout;
using Moq;
using NUnit.Framework;
using System;

namespace CheckoutTests
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void Can_Scan_Item()
        {
            var productRepository = new Mock<IProductRepository>();
            var checkout = new Checkout(productRepository.Object);
            checkout.Scan("A");
        }

        [Test]
        public void Can_Scan_Multiple_Items()
        {
            var productRepository = new Mock<IProductRepository>();
            var checkout = new Checkout(productRepository.Object);
            checkout.Scan("A");
            checkout.Scan("B");
        }

        [Test]
        public void Can_Get_Correct_Price_For_Single_Item()
        {
            var productRepository = new Mock<IProductRepository>();

            // Mock up Get method on Product repository - In a production app this would probably be a database
            productRepository.Setup(r => r.Get("A")).Returns(new Product { SKU = "A", UnitPrice = 50 });

            var checkout = new Checkout(productRepository.Object);
            checkout.Scan("A");

            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(50));
        }

        
    }
}
