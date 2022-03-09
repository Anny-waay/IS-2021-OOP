using System.Collections.Generic;
using Banks.Accounts;

namespace Banks
{
    public class TimeObserver
    {
        private readonly List<Account> _observers = new List<Account>();

        public void AddSubscriber(Account account)
        {
            _observers.Add(account);
        }

        public void NotifySubscribers(int days)
        {
            foreach (Account account in _observers)
            {
                account.MonthPercentsOrCommission(days);
            }
        }
    }
}