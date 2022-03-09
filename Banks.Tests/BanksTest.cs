using System;
using Banks.Accounts;
using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        private ICentralBank _centralBank;
        private Bank bank;
        private Client client1;
        private Client client2;
        private Client client3;
        
        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
            var bankBuilder = new BankBuilder();
            bankBuilder.SetName("Alpha-bank").SetDebitPersents(20).SetDepositMinPersents(15);
            bankBuilder.SetCreditCommission(10).SetCreditLimit(10000).SetDoubtSum(20000);
            bank = bankBuilder.GetBank();
            _centralBank.RegisterBank(bank);
            ClientBuilder clientBuilder1 = new ClientBuilder(); 
            client1 = clientBuilder1.SetSurname("Komova").SetName("Anna").SetAddress("Kotina 4").SetPassport("1234567").GetClient();
            ClientBuilder clientBuilder2 = new ClientBuilder(); 
            client2 = clientBuilder2.SetSurname("Golyakova").SetName("Tatiana").SetAddress("Kronva 43").SetPassport("1239567").GetClient();
            ClientBuilder clientBuilder3 = new ClientBuilder(); 
            client3 = clientBuilder3.SetSurname("Teterina").SetName("Maria").GetClient();
            _centralBank.AddClientToBank(bank, client1);
            _centralBank.AddClientToBank(bank, client2);
            _centralBank.AddClientToBank(bank, client3);
            _centralBank.CreateDebitAccount(bank, client1, 30000);
            _centralBank.CreateDepositAccount(bank, client1, 30000, 60);
            _centralBank.CreateCreditAccount(bank, client2, 10000);
            _centralBank.CreateDebitAccount(bank, client3, 30000);
        }

        [Test]
        public void TransferMoneyAndCancelOperation_BalanceOnBothAccountsWasChanged()
        {
            _centralBank.CentralBankTransfer(bank, client1.Accounts[0].Id.ToString(), bank, client2.Accounts[0].Id.ToString(), 200);
            Assert.AreEqual(client1.Accounts[0].Balance, 29800, "Client1 problem");
            Assert.AreEqual(client2.Accounts[0].Balance, 10200, "Client2 problem");
            _centralBank.CancelLastOperation(bank, client1.Accounts[0].Id.ToString());
            Assert.AreEqual(client1.Accounts[0].Balance, 30000, "Client1 cancel problem");
            Assert.AreEqual(client2.Accounts[0].Balance, 10000, "Client2 cancel problem");
        }

        [Test]
        public void RunTimeMachine_BalanceWasChanged()
        {
            _centralBank.RunTimeMachine(365);
            Assert.True(client1.Accounts[0].Balance > 36000);
        }
        
        [Test]
        public void ChangeDepositPercents_ClientHasMessage()
        {
            _centralBank.SubscribeClientForChanges(bank, client1);
            _centralBank.ChangeDepositMinPersents(bank, 31);
            Assert.AreEqual(((DepositAccount)client1.Accounts[1]).Percents, 0.31);
            Assert.AreEqual(client1.Messages.Count, 1);
        }
        
        [Test]
        public void WithdrawBigSumByDoubtClient_Exception()
        {
            Assert.Catch<BanksException>(() =>
            {
                _centralBank.CentralBankWithdraw(bank, client3.Accounts[0].Id.ToString(), 25000);
            });
        }
    }
}