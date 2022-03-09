namespace Shops.Entities
{
    public class Product
    {
        private string _productName;
        private int _productNumber;
        private int _price;
        public Product(string productName)
        {
            _productName = productName;
            _productNumber = 0;
            _price = 0;
        }

        public Product(string productName, int productNumber, int price)
        {
            _productName = productName;
            _productNumber = productNumber;
            _price = price;
        }

        public string ProductName => _productName;
        public int ProductNumber => _productNumber;
        public int Price => _price;

        public void AddNumberOfProduct(int productNumber)
        {
            _productNumber += productNumber;
        }

        public void RemoveProduct(int productNumber)
        {
            _productNumber -= productNumber;
        }

        public void SetPrice(int price)
        {
            _price = price;
        }
    }
}
