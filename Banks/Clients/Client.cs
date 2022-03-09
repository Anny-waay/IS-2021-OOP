using System.Collections.Generic;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Clients
{
    public class Client
    {
        public Client(string surname, string name, string passport, string address)
        {
            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(name))
            {
                throw new BanksException("You don't enter your surname or name!");
            }

            Surname = surname;
            Name = name;
            Passport = passport;
            Address = address;
            Accounts = new List<Account>();
            Messages = new List<string>();
        }

        public string Surname { get; }
        public string Name { get; }
        public string Passport { get; internal set; }
        public string Address { get; internal set; }
        public List<Account> Accounts { get; }
        public List<string> Messages { get; }
        public bool Varified => !string.IsNullOrEmpty(Passport) && !string.IsNullOrEmpty(Address);
    }
}