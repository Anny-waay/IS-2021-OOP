using Shops.Services;
using Shops.Tools;
using Shops.Entities;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _shopManager = new ShopManager();
        }

        [Test]
        public void DeliveryProductToShop_ShopContainsProduct()
        {
            Shop shop1 = _shopManager.CreateShop("Diksi", "Komsomolskay street, apart 4");
            Product lemon = _shopManager.AddProductToShop(shop1, "lemon", 20, 15);
            if (shop1.FindProduct(lemon.ProductName) == null)
            {
                Assert.Fail("No lemons in shop1. It is sad(");
            }
        }

        [Test]
        public void FindShopWithLowestPriceLemon_ShopWasFound()
        {
            Shop shop2 = _shopManager.CreateShop("Magnit", "Green street, apart 7");
            Product lemon = _shopManager.AddProductToShop(shop2, "lemon", 30, 10);
            if (!Equals(_shopManager.FindShopWithLowestPriceProduct(lemon.ProductName, 20), shop2))
            {
                Assert.Fail("Incorrect shop");
            }
        }

        [Test]
        public void PersonBuyProduct_MoneyAndNumberOfProductWasChanged()
        {
            int numberOfAppleBefore = 100;
            int appleToBuyNumber = 15;
            int moneyBefore = 500;

            Shop shop3 = _shopManager.CreateShop("Okay", "Lomonosova street, apart 2");
            _shopManager.AddProductToShop(shop3, "lemon", 5, 17);
            Product apple = _shopManager.AddProductToShop(shop3, "apple", numberOfAppleBefore, 25);

            var Tatiana = new Person("Tatiana", moneyBefore);
            _shopManager.BuyProduct(shop3, Tatiana, apple.ProductName, appleToBuyNumber);
            if (Tatiana.Money != (moneyBefore - shop3.FindProduct(apple.ProductName).Price * appleToBuyNumber)
                || shop3.FindProduct(apple.ProductName).ProductNumber != numberOfAppleBefore - appleToBuyNumber)
            {
                Assert.Fail("Incorrect buy");
            }
        }

        [Test]
        public void FindShopWithLowestPriceLemon_NotEnoughtProduct()
        {
            Shop shop4 = _shopManager.CreateShop("Okay", "Leninski street, apart 32");
            Product lemon = _shopManager.AddProductToShop(shop4, "lemon", 10, 12);
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.FindShopWithLowestPriceProduct(lemon.ProductName, 70);
            });
        }
    }
}
