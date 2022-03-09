using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;
using Banks.Transactions;

namespace Banks
{
    public class CentralBank : ICentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; }

        public void RegisterBank(Bank bank)
        {
            Banks.Add(bank);
        }

        public void AddClientToBank(Bank bank, Client client)
        {
            bank.AddClient(client);
        }

        public void CreateDebitAccount(Bank bank, Client client, double money)
        {
           bank.CreateDebitAccount(client, money);
        }

        public void CreateDepositAccount(Bank bank, Client client, double money, int termInDays)
        {
            bank.CreateDepositAccount(client, money, termInDays);
        }

        public void CreateCreditAccount(Bank bank, Client client, double money)
        {
            bank.CreateCreditAccount(client, money);
        }

        public void ChangeDebitPersents(Bank bank, double percents)
        {
            bank.ChangeDebitPersents(percents);
        }

        public void ChangeDepositMinPersents(Bank bank, double percents)
        {
            bank.ChangeDepositMinPersents(percents);
        }

        public void ChangeCreditLimit(Bank bank, double limit)
        {
            bank.ChangeCreditLimit(limit);
        }

        public void SubscribeClientForChanges(Bank bank, Client client)
        {
            bank.SubscribeClientForChanges(client);
        }

        public void CentralBankPut(Bank bank, string accountId, double money)
        {
            Account account = bank.FindAccount(accountId);
            if (account == null)
            {
                throw new BanksException("Incorrect Id, no such account!");
            }
            else
            {
               bank.BankPut(account, money);
            }
        }

        public void CentralBankWithdraw(Bank bank, string accountId, double money)
        {
            Account account = bank.FindAccount(accountId);
            if (account == null)
            {
                throw new BanksException("Incorrect Id, no such account!");
            }
            else
            {
                bank.BankWithdraw(account, money);
            }
        }

        public void CentralBankTransfer(Bank bank, string accountId, Bank bankPut, string accountPutId, double money)
        {
            Account account = bank.FindAccount(accountId);
            Account accountPut = bank.FindAccount(accountPutId);
            if (account == null || accountPut == null)
            {
                throw new BanksException("Incorrect Id of one of accounts!");
            }
            else
            {
                var transaction = new Transfer(account, accountPut);
                transaction.Execute(money);
            }
        }

        public void CancelLastOperation(Bank bank, string accountId)
        {
            Account account = bank.FindAccount(accountId);
            if (account == null)
            {
                throw new BanksException("Incorrect Id, no such account!");
            }

            account.TransactionHistory.Last().Cancel();
        }

        public void RunTimeMachine(int days)
        {
            foreach (Bank bank in Banks)
            {
                bank.NotifyBank(days);
            }
        }
    }
}