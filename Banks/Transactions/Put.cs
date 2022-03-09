using Banks.Accounts;

namespace Banks.Transactions
{
    public class Put : Transaction
    {
        public Put(Account account)
            : base(account) { }

        public override void Execute(double money)
        {
            BackupMoney = money;
            BankAccount.PutMoney(money);
            BankAccount.TransactionHistory.Add(this);
        }

        public override void Cancel()
        {
            BankAccount.WithdrawMoney(BackupMoney);
        }
    }
}