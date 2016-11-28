using System;
using System.Collections.Generic;

namespace LateRoomsCheckout
{
    public class Checkout : ICheckout
    {
        private List<string> _items = new List<string>();

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string sku)
        {
            _items.Add(sku);
        }
    }
}
