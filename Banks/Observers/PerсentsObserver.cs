using System.Collections.Generic;
using Banks.Clients;

namespace Banks
{
    public class PerсentsObserver
    {
        private readonly List<Client> _debitObservers = new List<Client>();
        private readonly List<Client> _depositObservers = new List<Client>();
        private readonly List<Client> _creditObservers = new List<Client>();

        public void AddDebitSubscriber(Client client)
        {
            _debitObservers.Add(client);
        }

        public void AddDepositSubscriber(Client client)
        {
            _depositObservers.Add(client);
        }

        public void AddCreditSubscriber(Client client)
        {
            _creditObservers.Add(client);
        }

        public void NotifyDebitSubscribers()
        {
            foreach (Client client in _debitObservers)
            {
                client.Messages.Add("Debit perсents was changed!");
            }
        }

        public void NotifyDepositSubscribers()
        {
            foreach (Client client in _depositObservers)
            {
                client.Messages.Add("Deposit perсents was changed!");
            }
        }

        public void NotifyCreditSubscribers()
        {
            foreach (Client client in _creditObservers)
            {
                client.Messages.Add("Credit limit was changed!");
            }
        }
    }
}