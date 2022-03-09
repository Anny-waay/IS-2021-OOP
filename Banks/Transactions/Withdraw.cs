using Banks.Accounts;

namespace Banks.Transactions
{
    public class Withdraw : Transaction
    {
        public Withdraw(Account account)
            : base(account) { }
        public override void Execute(double money)
        {
            BackupMoney = money;
            BankAccount.WithdrawMoney(money);
            BankAccount.TransactionHistory.Add(this);
        }

        public override void Cancel()
        {
            BankAccount.PutMoney(BackupMoney);
        }
    }
}