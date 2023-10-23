using Bank.Class;
using Bank.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Bank
{
    internal class Manager
    {
        public static void Start()
        {
            List<ICard> cards = new List<ICard>();
            IClient client = new Client("Nika", "Ignatenko", "0667858087", "Povitroflots'kyi Prosp. 38", "380583018654");
            IAccount account = new Account(client, Currency.uah);
            while (!AccountExist(account))
            {
                Console.Clear();
                Console.WriteLine("!INVALID IPN!");
            }
            while (true)
            {
                Menu();
                switch (Choose())
                {
                    case 0:
                        do
                        {
                            Console.Clear();
                            CardList(cards, account);
                        }
                        while (ManageCards(cards, account) != 0);
                        break;
                    case 1:
                        do
                        {
                            Console.Clear();
                            ServiceList(account);
                        }
                        while (Console.ReadKey(intercept: true).KeyChar != '0');
                        break;
                    case 2:
                        do
                        {
                            Console.Clear();
                            ServiceMenu();
                        } while (ServiceCreate(account)!=0);
                        break;
                    case 3:
                        do
                        {
                            Console.Clear();
                            CardMenu();
                        }while(CardCreate(account,cards)!=0);
                        break;
                    case 4:
                        Console.Clear();
                        foreach (ICard card in cards.Where(c => c.CardHolder == account))
                        {
                            card.OnOperationAdded += Program.OtherMethod;
                        }
                        Console.WriteLine("Notifications on!");
                        Console.ReadKey();
                        break;
                    case 5:
                        return;
                }
            }
        }
        static int CardCreate(IAccount account,List<ICard> cards)
        {
            if (uint.TryParse(Console.ReadKey(intercept: true).KeyChar.ToString(), out uint choose) && choose <= 2)
            {
                if (choose == 0)
                {
                    return 0;
                }
                AddCard(account, cards, choose);
            }
            return 1;
        }
        static void AddCard(IAccount account, List<ICard> cards, uint choose)
        {
            string pin;
            switch (choose)
            {
                case 1:
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Credit card");
                        Console.Write("Create pin:");
                        pin = Console.ReadLine();
                    } while (pin.Length>4);
                    account.CreateCard(true, cards, pin);
                    return;
                case 2:
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Debit card");
                        Console.Write("Create pin:");
                        pin = Console.ReadLine();
                    } while (pin.Length > 4);
                    account.CreateCard(false, cards, pin);
                    return;
            }
        }
        static void CardMenu()
        {
            Console.WriteLine("0.Return.\n" +
                              "1.Create credit card.\n" +
                              "2.Create deposit card.");
        }
        static int ServiceCreate(IAccount account)
        {
            if (uint.TryParse(Console.ReadKey(intercept: true).KeyChar.ToString(), out uint choose) && choose <= 2)
            {
                if (choose == 0)
                {
                    return 0;
                }
                AddService(account,choose);
            }
            return 1;
        }
        static void AddService(IAccount account,uint choose)
        {
            int term;
            decimal sum;
            switch (choose)
            {
                case 1:
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter credit term:");
                    } while (!int.TryParse(Console.ReadLine(), out term));
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter credit sum:");
                    } while (!decimal.TryParse(Console.ReadLine(), out sum));
                    account.AddService(true, term, sum, 4.5f);
                    return;
                case 2:
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter deposit term:");
                    } while (!int.TryParse(Console.ReadLine(), out term));
                    do
                    {
                        Console.Clear();
                        Console.Write("Enter deposit sum:");
                    } while (!decimal.TryParse(Console.ReadLine(), out sum));
                    account.AddService(false, term, sum, 2.5f);
                    return;
            }
        }
        static void ServiceMenu()
        {
            Console.WriteLine("0.Return.\n" +
                              "1.Create credit.\n" +
                              "2.Create deposit.");
        }
        static void ServiceList(IAccount account)
        {
            Console.WriteLine("Your services:");
            Console.WriteLine("0.Return.");
            foreach(IService service in account.Services)
            {
                
                Console.WriteLine(service);
            }
        }
        static void CardList(List<ICard> cards, IAccount account)
        {
            List<ICard> temp = cards.Where(c=>c.CardHolder == account).ToList();
            Console.WriteLine($"Balance:{account.Balance}{account.Currency}");
            Console.WriteLine("Your cards:");
            Console.WriteLine("0.Return.");
            foreach (ICard card in temp)
            {
                Console.WriteLine($"{temp.IndexOf(card)+1}.{card}");
            }
        }
        static int ManageCards(List<ICard> cards,IAccount account)
        {
            List<ICard> temp = cards.Where(c => c.CardHolder == account).ToList();
            if (uint.TryParse(Console.ReadKey(intercept: true).KeyChar.ToString(), out uint choose) && choose <= temp.Count)
            {
                if (choose == 0)
                {
                    return 0;
                }
                ManageChosen(cards, temp[(int)choose-1]);
            }
            return 1;
        }
        static void ManageChosen(List<ICard> cards, ICard card)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(card);
                Console.WriteLine("0.Return.\n" +
                                  "1.Withdraw.\n" +
                                  "2.Deposit.");
                ShowOperations(card);
                decimal sum=0;
                switch (Choose())
                {
                    case 0:
                        return;
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.Write("Enter withdraw sum:");
                        }while(!decimal.TryParse(Console.ReadLine(), out sum));
                        if(!card.AddOperation(true, sum))
                        {
                            Console.WriteLine("Not enough money for withdrawal!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Success!");
                            Console.ReadKey();
                        }
                        return;
                    case 2:
                        do
                        {
                            Console.Clear();
                            Console.Write("Enter deposit sum:");
                        } while (!decimal.TryParse(Console.ReadLine(), out sum));
                        if (card.AddOperation(false, sum))
                        {
                            Console.WriteLine("Success!");
                            Console.ReadKey();
                        }
                        return;
                }
            }
        }
        static void ShowOperations(ICard card)
        {
            List<IOperation> operations = card.CardHolder.Operations.Where(o=>o.Card==card).ToList();
            foreach (IOperation operation in operations)
            {
                Console.WriteLine(operation);
            }
        }
        static int Choose()
        {
            while (true)
            {
                if (uint.TryParse(Console.ReadKey(intercept: true).KeyChar.ToString(), out uint choose))
                {
                    return (int)choose;
                }
            }
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Select option:\n" +
                              "0.See my cards.\n" +
                              "1.See my services.\n" +
                              "2.Create new service.\n" +
                              "3.Create new card.\n" +
                              "4.Email/phone notification.\n" +
                              "5.Exit.");
        }
        static bool AccountExist(IAccount account)
        {
            Console.Write("Enter your IPN:");
            string ipn = Console.ReadLine();
            if (account.Client.IPN==ipn)
            {
                return true;
            }
            return false;
        }
    }
}
