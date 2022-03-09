using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(Bank bankOwner, Client owner, double balance, double doubtSum)
            : base(bankOwner, owner, balance, doubtSum)
        {
            Cashback = 0;
        }

        public double Percents { get; internal set; }
        public double Cashback { get; protected set; }

        public void SetPercents(double percents)
        {
            Percents = percents / 100.0;
        }

        public override void WithdrawMoney(double money)
        {
            if (money > DoubtSum && !Owner.Varified)
            {
                throw new BanksException("The sum is too big! Identify your account!");
            }

            if (money > Balance)
            {
                throw new BanksException("Not enough money!");
            }

            Balance -= money;
        }

        public override void MonthPercentsOrCommission(int days)
        {
            for (int i = 1; i <= days; ++i)
            {
                Cashback += Balance * Percents / Year;
                if (DaysFromCreation % Month == 0)
                {
                    Balance += Cashback;
                    Cashback = 0;
                }

                DaysFromCreation++;
            }
        }
    }
}