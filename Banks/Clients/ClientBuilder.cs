namespace Banks.Clients
{
    public class ClientBuilder
    {
        private string _surname;
        private string _name;
        private string _passport;
        private string _address;
        public ClientBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public Client GetClient() => new Client(_name, _surname, _address, _passport);
    }
}