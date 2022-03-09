using System;
using Banks.BankModel;

namespace Banks.ConsoleInterface
{
    public class View
    {
        private Controller _controller;

        public View()
        {
            _controller = new Controller();
        }

        public void Start()
        {
            Console.WriteLine("Who are you? Choose:");
            Console.WriteLine("1 Bank owner");
            Console.WriteLine("2 Client");
            var answer = Console.ReadLine();
            switch (answer)
            {
                    case "1":
                        BankOwnerActions();
                        break;
                    case "2":
                        ClientActions();
                        break;
                    default:
                        Console.WriteLine("Incorrect input! Try again.");
                        Start();
                        break;
            }
        }

        public void ClientActions()
        {
            Console.WriteLine("What do you want? Choose:");
            Console.WriteLine("1 Register");
            Console.WriteLine("2 Open account");
            Console.WriteLine("3 Put money");
            Console.WriteLine("4 Withdraw money");
            Console.WriteLine("5 Transfer money");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    RegisterClient();
                    break;
                case "2":
                    OpenAccount();
                    break;
                case "3":
                    PutMoney();
                    break;
                case "4":
                    WithdrawMoney();
                    break;
                case "5":
                    TransferMoney();
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }

            Console.WriteLine("Do you want something else? Choose:");
            Console.WriteLine("1 Yes");
            Console.WriteLine("2 No");
            answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    ClientActions();
                    break;
                case "2":
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }

        public void RegisterClient()
        {
            Console.WriteLine("Choose bank where you want to register:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Write your surname:");
            var surname = Console.ReadLine();
            Console.WriteLine("Write your name:");
            var name = Console.ReadLine();
            Console.WriteLine("Do you want to add passport and address? Else you will have limit in transactions.");
            Console.WriteLine("1 Yes");
            Console.WriteLine("2 No");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Console.WriteLine("Write your passport:");
                    var passport = Console.ReadLine();
                    Console.WriteLine("Write your address:");
                    var address = Console.ReadLine();
                    _controller.CreateFullClient(surname, name, passport, address, bankNumber);
                    break;
                case "2":
                    _controller.CreateBasicClient(surname, name, bankNumber);
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }

        public void OpenAccount()
        {
            Console.WriteLine("Choose bank where you are registered and want to open account:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Write your surname to find your profile:");
            var surname = Console.ReadLine();
            Console.WriteLine("How much money do you want to put?");
            var money = double.Parse(Console.ReadLine());
            Console.WriteLine("What account do you want to create? Choose:");
            Console.WriteLine("1 Debit account");
            Console.WriteLine("2 Deposit account");
            Console.WriteLine("3 Credit account");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    _controller.CreateDebitAccount(surname, bankNumber, money);
                    break;
                case "2":
                    Console.WriteLine("Write term in days:");
                    var term = int.Parse(Console.ReadLine());
                    _controller.CreateDepositAccount(surname, bankNumber, money, term);
                    break;
                case "3":
                    _controller.CreateCreditAccount(surname, bankNumber, money);
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }

        public void PutMoney()
        {
            Console.WriteLine("Choose your bank:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Write your account Id");
            var id = Console.ReadLine();
            Console.WriteLine("How much money do you want to put?");
            var money = double.Parse(Console.ReadLine());
            _controller.PutMoney(bankNumber, id, money);
        }

        public void WithdrawMoney()
        {
            Console.WriteLine("Choose your bank:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Write your account Id");
            var id = Console.ReadLine();
            Console.WriteLine("How much money do you want to withdraw?");
            var money = double.Parse(Console.ReadLine());
            _controller.WithdrawMoney(bankNumber, id, money);
        }

        public void TransferMoney()
        {
            Console.WriteLine("Choose your bank:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Choose your recipient bank:");
            _controller.ShowBanks();
            var bankNumberPut = int.Parse(Console.ReadLine());
            Console.WriteLine("Write your account Id");
            var id = Console.ReadLine();
            Console.WriteLine("Write your recipient account Id");
            var idPut = Console.ReadLine();
            Console.WriteLine("How much money do you want to transfer?");
            var money = double.Parse(Console.ReadLine());
            _controller.TransferMoney(bankNumber, id, bankNumberPut, idPut, money);
        }

        public void BankOwnerActions()
        {
            Console.WriteLine("What do you want? Choose:");
            Console.WriteLine("1 Register");
            Console.WriteLine("2 Change percents/credit limit");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    RegisterBank();
                    break;
                case "2":
                    OpenAccount();
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }

        public void RegisterBank()
        {
            Console.WriteLine("Write name of your bank:");
            var name = Console.ReadLine();
            Console.WriteLine("Write debit percents for your bank:");
            var debitPercents = double.Parse(Console.ReadLine());
            Console.WriteLine("Write deposit percents for your bank:");
            var depositPercents = double.Parse(Console.ReadLine());
            Console.WriteLine("Write credit commission for your bank:");
            var creditCommission = double.Parse(Console.ReadLine());
            Console.WriteLine("Write credit limit for your bank:");
            var creditLimit = double.Parse(Console.ReadLine());
            Console.WriteLine("Write maximum sum for not varified clients:");
            var doubtSum = double.Parse(Console.ReadLine());
            _controller.RegisterBank(name, debitPercents, depositPercents, creditCommission, creditLimit, doubtSum);
        }

        public void ChangePersentsLimit()
        {
            Console.WriteLine("Choose your bank:");
            _controller.ShowBanks();
            var bankNumber = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("What do you want to change?");
            Console.WriteLine("1 Debit percents");
            Console.WriteLine("2 Deposit percents");
            Console.WriteLine("3 Credit limit");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Console.WriteLine("Write new debit percents:");
                    var debitPercents = double.Parse(Console.ReadLine());
                    _controller.ChangeDebitPercents(bankNumber, debitPercents);
                    break;
                case "2":
                    Console.WriteLine("Write new deposit percents:");
                    var depositPercents = double.Parse(Console.ReadLine());
                    _controller.ChangeDepositPercents(bankNumber, depositPercents);
                    break;
                case "3":
                    Console.WriteLine("Write new credit limit:");
                    var limit = double.Parse(Console.ReadLine());
                    _controller.ChangeCreditLimit(bankNumber, limit);
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }
    }
}