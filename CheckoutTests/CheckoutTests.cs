using LateRoomsCheckout;
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
            var checkout = new Checkout();
            checkout.Scan("A");
        }

        [Test]
        public void Can_Scan_Multiple_Items()
        {
            var checkout = new Checkout();
            checkout.Scan("A");
            checkout.Scan("B");
        }
    }
}
