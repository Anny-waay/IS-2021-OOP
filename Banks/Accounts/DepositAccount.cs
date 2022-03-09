using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(Bank bankOwner, Client owner, double balance, double doubtSum, int termInDays)
            : base(bankOwner, owner, balance, doubtSum)
        {
            TermInDays = termInDays;
            Cashback = 0;
        }

        public double Percents { get; protected set; }
        public double Cashback { get; protected set; }
        public int TermInDays { get; }

        public void SetPercents(double percents)
        {
            if (Balance > 100000.0)
            {
                percents += 1;
            }
            else
            {
                if (Balance > 50000.0)
                {
                    percents += 0.5;
                }
            }

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

            if (DaysFromCreation < TermInDays)
            {
                throw new BanksException("The term has not expired yet, you can not withdraw / transfer money");
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