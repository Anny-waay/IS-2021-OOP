namespace Shops.Entities
{
    public class Person
    {
        private string _name;
        private int _money;
        public Person(string name, int money)
        {
            _name = name;
            _money = money;
        }

        public int Money => _money;

        public void Buy(int price, int numberOfProduct)
        {
            _money -= price * numberOfProduct;
        }
    }
}
