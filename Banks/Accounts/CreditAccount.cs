using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(Bank bankOwner, Client owner, double balance, double doubtSum, double limit, double commission)
            : base(bankOwner, owner, balance, doubtSum)
        {
            Limit = limit;
            Commission = commission;
        }

        public double Limit { get; internal set; }
        public double Commission { get; }

        public override void WithdrawMoney(double money)
        {
            if (money > DoubtSum && !Owner.Varified)
            {
                throw new BanksException("The sum is too big! Identify your account!");
            }

            if (money + Commission > Limit + Balance)
            {
                throw new BanksException("Not enough money!");
            }

            if (Balance < 0)
            {
                Balance -= money + Commission;
            }
            else
            {
                Balance -= money;
            }
        }

        public override void MonthPercentsOrCommission(int days)
        {
            if (Balance < 0)
            {
                for (int i = 1; i <= days; ++i)
                {
                    Balance -= Commission;
                }
            }
        }
    }
}