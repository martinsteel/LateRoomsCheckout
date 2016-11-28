using System;

namespace LateRoomsCheckout
{
    class Checkout : ICheckout
    {
        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string sku)
        {
            throw new NotImplementedException();
        }
    }
}
