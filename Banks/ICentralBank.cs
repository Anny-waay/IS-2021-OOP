using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.BankModel;
using Banks.Clients;
using Banks.Tools;
using Banks.Transactions;

namespace Banks
{
    public interface ICentralBank
    {
        void RegisterBank(Bank bank);

        void AddClientToBank(Bank bank, Client client);

        void CreateDebitAccount(Bank bank, Client client, double money);

        void CreateDepositAccount(Bank bank, Client client, double money, int termInDays);

        void CreateCreditAccount(Bank bank, Client client, double money);

        void ChangeDebitPersents(Bank bank, double percents);

        void ChangeDepositMinPersents(Bank bank, double percents);

        void ChangeCreditLimit(Bank bank, double limit);

        void SubscribeClientForChanges(Bank bank, Client client);

        void CentralBankPut(Bank bank, string accountId, double money);

        void CentralBankWithdraw(Bank bank, string accountId, double money);

        void CentralBankTransfer(Bank bank, string accountId, Bank bankPut, string accountPutId, double money);

        void CancelLastOperation(Bank bank, string accountId);

        void RunTimeMachine(int days);
    }
}