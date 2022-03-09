namespace Banks.BankModel
{
    public class BankBuilder
    {
        private string _bankName;
        private double _debitPercents;
        private double _depositMinPercents;
        private double _creditСommission;
        private double _creditLimit;
        private double _doubtSum;

        public BankBuilder SetName(string name)
        {
            _bankName = name;
            return this;
        }

        public BankBuilder SetDebitPersents(double persents)
        {
            _debitPercents = persents;
            return this;
        }

        public BankBuilder SetDepositMinPersents(double persents)
        {
            _depositMinPercents = persents;
            return this;
        }

        public BankBuilder SetCreditCommission(double commission)
        {
            _creditСommission = commission;
            return this;
        }

        public BankBuilder SetCreditLimit(double limit)
        {
            _creditLimit = limit;
            return this;
        }

        public BankBuilder SetDoubtSum(double sum)
        {
            _doubtSum = sum;
            return this;
        }

        public Bank GetBank() =>
            new Bank(_bankName, _debitPercents, _depositMinPercents, _creditСommission, _creditLimit, _doubtSum);
    }
}