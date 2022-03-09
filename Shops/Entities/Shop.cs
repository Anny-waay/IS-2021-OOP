using System.Collections.Generic;
namespace Shops.Entities
{
    public class Shop
    {
        private static int idGenerator = 0;
        private string _shopName;
        private string _address;
        private int _id;
        private List<Product> _products;
        public Shop(string name, string address)
        {
            _shopName = name;
            _address = address;
            _id = idGenerator++;
            _products = new List<Product>();
        }

        public List<Product> Products => _products;

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public Product FindProduct(string productName)
        {
            foreach (Product foundProduct in _products)
            {
                if (foundProduct.ProductName == productName)
                {
                    return foundProduct;
                }
            }

            return null;
        }
    }
}
