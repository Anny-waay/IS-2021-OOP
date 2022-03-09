using Shops.Entities;
namespace Shops.Services
{
    public interface IShopManager
    {
        Shop CreateShop(string shopName, string address);
        Product AddProductToShop(Shop shop, string productName, int productNumber, int price);
        void BuyProduct(Shop shop, Person person, string productName, int productNumber);
        Shop FindShopWithLowestPriceProduct(string productName, int productNumber);
    }
}
