using System.Collections.Generic;
using Shops.Entities;
using Shops.Tools;
namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _shops;
        public ShopManager()
        {
            _shops = new List<Shop>();
        }

        public Shop CreateShop(string shopName, string address)
        {
            var shop = new Shop(shopName, address);
            _shops.Add(shop);
            return shop;
        }

        public Product AddProductToShop(Shop shop, string productName, int productNumber, int price)
        {
            var result = new Product(productName, productNumber, price);
            foreach (Product foundProduct in shop.Products)
            {
                if (foundProduct.ProductName == productName)
                {
                    foundProduct.AddNumberOfProduct(productNumber);
                    if (foundProduct.Price == 0)
                    {
                        foundProduct.SetPrice(price);
                    }

                    result = foundProduct;
                    return result;
                }
            }

            shop.AddProduct(result);
            return result;
        }

        public void BuyProduct(Shop shop, Person person, string productName, int productNumber)
        {
            Product foundProduct = shop.FindProduct(productName);
            if (foundProduct != null)
            {
                if (foundProduct.ProductNumber > productNumber)
                {
                    if (person.Money > foundProduct.Price * productNumber)
                    {
                        person.Buy(foundProduct.Price, productNumber);
                        foundProduct.RemoveProduct(productNumber);
                    }
                    else
                    {
                        throw new ShopException("Not enought money!");
                    }
                }
                else
                {
                    throw new ShopException("Not enought product in this shop!");
                }
            }
            else
            {
                throw new ShopException("No such product in this shop!");
            }
        }

        public Shop FindShopWithLowestPriceProduct(string productName, int productNumber)
        {
            bool found = false;
            int minPrice = int.MaxValue;
            Shop result = null;
            foreach (Shop shop in _shops)
            {
                Product foundProduct = shop.FindProduct(productName);
                if (foundProduct != null)
                {
                    found = true;
                    if (foundProduct.Price < minPrice && foundProduct.ProductNumber >= productNumber)
                    {
                        result = shop;
                    }
                }
            }

            if (result == null && found)
            {
                throw new ShopException("Not enought product in all shops!");
            }
            else
            {
                if (!found)
                {
                    throw new ShopException("No such product in all shops!");
                }
            }

            return result;
        }
    }
}
