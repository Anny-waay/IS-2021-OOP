using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BankModel;
using Banks.Clients;

namespace Banks.ConsoleInterface
{
    public class Controller
    {
        private CentralBank _centralBank;

        public Controller()
        {
            _centralBank = new CentralBank();
        }

        public void ShowBanks()
        {
            for (int i = 0; i < _centralBank.Banks.Count; i++)
            {
                Console.WriteLine($"{i + 1} {_centralBank.Banks[i].BankName}");
            }
        }

        public void CreateBasicClient(string surname, string name, int bankNumber)
        {
            ClientBuilder clientBuilder = new ClientBuilder();
            Client client = clientBuilder.SetSurname(surname).SetName(name).GetClient();
            _centralBank.AddClientToBank(_centralBank.Banks[bankNumber], client);
        }

        public void CreateFullClient(string surname, string name, string passport, string address, int bankNumber)
        {
            ClientBuilder clientBuilder = new ClientBuilder();
            Client client = clientBuilder.SetSurname(surname).SetName(name).SetPassport(passport).SetAddress(address).GetClient();
            _centralBank.AddClientToBank(_centralBank.Banks[bankNumber], client);
        }

        public Client FindClient(string surname, int bankNumber)
        {
            foreach (Client client in _centralBank.Banks[bankNumber].Clients)
            {
                if (client.Surname == surname)
                {
                    return client;
                }
            }

            return null;
        }

        public void CreateDebitAccount(string surname, int bankNumber, double money)
        {
            _centralBank.CreateDebitAccount(_centralBank.Banks[bankNumber], FindClient(surname, bankNumber), money);
            Console.WriteLine($"Your account id is {FindClient(surname, bankNumber).Accounts.Last().Id}");
        }

        public void CreateDepositAccount(string surname, int bankNumber, double money, int term)
        {
            _centralBank.CreateDepositAccount(_centralBank.Banks[bankNumber], FindClient(surname, bankNumber), money, term);
            Console.WriteLine($"Your account id is {FindClient(surname, bankNumber).Accounts.Last().Id}");
        }

        public void CreateCreditAccount(string surname, int bankNumber, double money)
        {
            _centralBank.CreateCreditAccount(_centralBank.Banks[bankNumber], FindClient(surname, bankNumber), money);
            Console.WriteLine($"Your account id is {FindClient(surname, bankNumber).Accounts.Last().Id}");
        }

        public void PutMoney(int bankNumber, string accountId, double money)
        {
            _centralBank.CentralBankPut(_centralBank.Banks[bankNumber], accountId, money);
        }

        public void WithdrawMoney(int bankNumber, string accountId, double money)
        {
            _centralBank.CentralBankWithdraw(_centralBank.Banks[bankNumber], accountId, money);
        }

        public void TransferMoney(int bankNumber, string accountId, int bankNumberPut, string accountIdPut, double money)
        {
            _centralBank.CentralBankTransfer(_centralBank.Banks[bankNumber], accountId,  _centralBank.Banks[bankNumberPut], accountIdPut, money);
        }

        public void RegisterBank(string name, double debitPercent, double depositPercents, double creditCommission, double creditLimit, double doubtSum)
        {
            var bankBuilder = new BankBuilder();
            bankBuilder.SetName(name).SetDebitPersents(debitPercent).SetDepositMinPersents(depositPercents);
            bankBuilder.SetCreditCommission(creditCommission).SetCreditLimit(creditLimit).SetDoubtSum(doubtSum);
            var bank = bankBuilder.GetBank();
            _centralBank.RegisterBank(bank);
        }

        public void ChangeDebitPercents(int bankNumber, double persents)
        {
            _centralBank.ChangeDebitPersents(_centralBank.Banks[bankNumber], persents);
        }

        public void ChangeDepositPercents(int bankNumber, double persents)
        {
            _centralBank.ChangeDepositMinPersents(_centralBank.Banks[bankNumber], persents);
        }

        public void ChangeCreditLimit(int bankNumber, double limit)
        {
            _centralBank.ChangeCreditLimit(_centralBank.Banks[bankNumber], limit);
        }
    }
}