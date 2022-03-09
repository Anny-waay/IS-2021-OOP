using Banks.Accounts;

namespace Banks.Transactions
{
    public class Transfer : Transaction
    {
        public Transfer(Account account, Account accountPut)
            : base(account)
        {
            BankAccountPut = accountPut;
        }

        public Account BankAccountPut { get; }

        public override void Execute(double money)
        {
            BackupMoney = money;
            BankAccount.WithdrawMoney(money);
            BankAccountPut.PutMoney(money);
            BankAccount.TransactionHistory.Add(this);
            BankAccountPut.TransactionHistory.Add(this);
        }

        public override void Cancel()
        {
            BankAccountPut.WithdrawMoney(BackupMoney);
            BankAccount.PutMoney(BackupMoney);
        }
    }
}