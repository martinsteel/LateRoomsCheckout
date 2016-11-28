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
            // Group purchased items by SKU so we can calculate discounts.
            var products = _items.GroupBy(x => x.SKU, (sku, items) => 
                new {
                    Quantity = items.Count(),
                    Product = items.First()
                });

            int total = 0;
            foreach (var product in products)
            {
                if (product.Product.SpecialQuantity == 0) // no discount
                {
                    total += product.Quantity * product.Product.UnitPrice;
                }
                else // discounted product
                {
                    int fullPriceCount = product.Quantity % product.Product.SpecialQuantity;
                    int discountCount = product.Quantity / product.Product.SpecialQuantity;

                    total += fullPriceCount * product.Product.UnitPrice;
                    total += discountCount * product.Product.SpecialPrice;
                }
            }
            return total;
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