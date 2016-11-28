using System;
using System.Collections.Generic;
using System.Linq;

namespace LateRoomsCheckout
{
    public class Checkout : ICheckout
    {
        private readonly IProductRepository _productRepository;
        private List<Product> _items = new List<Product>();

        public Checkout(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int GetTotalPrice()
        {
            return _items.Sum(x => x.UnitPrice);
        }

        public bool Scan(string sku)
        {
            var product = _productRepository.Get(sku);

            if (product == null)
                return false;

            _items.Add(product);
            return true;            
        }
    }
}
