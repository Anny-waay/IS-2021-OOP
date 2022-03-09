using Banks.Accounts;

namespace Banks.Transactions
{
    public abstract class Transaction
    {
        protected Transaction(Account account)
        {
            BankAccount = account;
        }

        public double BackupMoney { get; protected set; }
        public Account BankAccount { get; }

        public virtual void Execute(double money) { }
        public virtual void Cancel() { }
    }
}