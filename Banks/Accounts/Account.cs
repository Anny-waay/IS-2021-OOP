using System;
using System.Collections.Generic;
using Banks.BankModel;
using Banks.Clients;
using Banks.Transactions;

namespace Banks.Accounts
{
    public abstract class Account
    {
        protected const double Year = 365.0;
        protected const double Month = 30.0;
        protected Account(Bank bankOwner, Client owner, double balance, double doubtSum)
        {
            BankOwner = bankOwner;
            Owner = owner;
            Id = Guid.NewGuid();
            Balance = balance;
            DoubtSum = doubtSum;
            DaysFromCreation = 1;
        }

        public Bank BankOwner { get; }
        public Client Owner { get; }
        public Guid Id { get; }
        public double Balance { get; protected set; }
        public double DoubtSum { get; }
        public int DaysFromCreation { get; protected set; }
        public List<Transaction> TransactionHistory { get; } = new List<Transaction>();

        public void PutMoney(double money)
        {
            Balance += money;
        }

        public virtual void WithdrawMoney(double money) { }
        public virtual void MonthPercentsOrCommission(int days) { }
    }
}