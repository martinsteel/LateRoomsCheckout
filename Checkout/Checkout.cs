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

        public void Scan(string sku)
        {
            var product = _productRepository.Get(sku);
            _items.Add(product);
        }
    }
}
