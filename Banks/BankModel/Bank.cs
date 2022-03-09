using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankModel
{
    public class Bank
    {
        private TimeObserver _accountTimeObserver = new TimeObserver();
        private PerсentsObserver _perсentsObserver = new PerсentsObserver();

        public Bank(string name, double debitPercent, double depositPercents, double creditCommission, double creditLimit, double doubtSum)
        {
            Clients = new List<Client>();
            Accounts = new List<Account>();
            BankName = name;
            DebitPercents = debitPercent;
            DepositMinPercents = depositPercents;
            CreditСommission = creditCommission;
            CreditLimit = creditLimit;
            DoubtSum = doubtSum;
        }

        public string BankName { get; }
        public List<Client> Clients { get; }
        public List<Account> Accounts { get; }
        public double DebitPercents { get; private set; }
        public double DepositMinPercents { get; private set; }
        public double CreditСommission { get; }
        public double CreditLimit { get; private set; }
        public double DoubtSum { get; }

        public void AddClient(Client client)
        {
            if (client != null)
            {
                Clients.Add(client);
            }
            else
            {
                throw new BanksException("Incorrect client!");
            }
        }

        public void CreateDebitAccount(Client client, double money)
        {
            var newAccount = new DebitAccount(this, client, money, DoubtSum);
            newAccount.SetPercents(DebitPercents);
            Accounts.Add(newAccount);
            client.Accounts.Add(newAccount);
            _accountTimeObserver.AddSubscriber(newAccount);
        }

        public void CreateDepositAccount(Client client, double money, int termInDays)
        {
            var newAccount = new DepositAccount(this, client, money, DoubtSum, termInDays);
            newAccount.SetPercents(DepositMinPercents);
            Accounts.Add(newAccount);
            client.Accounts.Add(newAccount);
            _accountTimeObserver.AddSubscriber(newAccount);
        }

        public void CreateCreditAccount(Client client, double money)
        {
            var newAccount = new CreditAccount(this, client, money, DoubtSum, CreditLimit, CreditСommission);
            Accounts.Add(newAccount);
            client.Accounts.Add(newAccount);
            _accountTimeObserver.AddSubscriber(newAccount);
        }

        public Account FindAccount(string accountId)
        {
            foreach (Account account in Accounts)
            {
                if (account.Id.ToString() == accountId)
                {
                    return account;
                }
            }

            return null;
        }

        public void ChangeDebitPersents(double percents)
        {
            DebitPercents = percents;
            foreach (Account account in Accounts)
            {
                if (account is DebitAccount)
                {
                    ((DebitAccount)account).SetPercents(percents);
                }
            }

            _perсentsObserver.NotifyDebitSubscribers();
        }

        public void ChangeDepositMinPersents(double percents)
        {
            DepositMinPercents = percents;
            foreach (Account account in Accounts)
            {
                if (account is DepositAccount)
                {
                    ((DepositAccount)account).SetPercents(percents);
                }
            }

            _perсentsObserver.NotifyDepositSubscribers();
        }

        public void ChangeCreditLimit(double limit)
        {
            CreditLimit = limit;
            foreach (Account account in Accounts)
            {
                if (account is CreditAccount)
                {
                    ((CreditAccount)account).Limit = limit;
                }
            }

            _perсentsObserver.NotifyCreditSubscribers();
        }

        public void SubscribeClientForChanges(Client client)
        {
            foreach (Account account in client.Accounts)
            {
                if (account is DebitAccount)
                {
                    _perсentsObserver.AddDebitSubscriber(client);
                }

                if (account is DepositAccount)
                {
                    _perсentsObserver.AddDepositSubscriber(client);
                }

                if (account is CreditAccount)
                {
                    _perсentsObserver.AddCreditSubscriber(client);
                }
            }
        }

        public void BankPut(Account account, double money)
        {
            var transaction = new Put(account);
            transaction.Execute(money);
        }

        public void BankWithdraw(Account account, double money)
        {
            var transaction = new Withdraw(account);
            transaction.Execute(money);
        }

        public void NotifyBank(int days)
        {
            _accountTimeObserver.NotifySubscribers(days);
        }
    }
}