using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class Account:IAccount
    {
        IClient _client;
        Currency _currency;
        List<IService> _services;
        string _iban;
        List<IOperation> _operations;
        public IClient Client { get { return _client; } }
        public Currency Currency { get { return _currency; } }
        public decimal Balance { get; set; }
        public List<IService> Services { get { return _services; } }
        public string IBAN { get { return _iban; } }
        public List<IOperation> Operations { get {  return _operations; } }
        public Account(IClient client, Currency currency)
        {
            _client = client;
            _currency = currency;
            _services = new List<IService>();
            _operations = new List<IOperation>();
            Balance = 0;
            _iban = GenerateIBAN();
        }
        string GenerateIBAN()
        {
            Random rand = new Random();

            string randomIBAN = "DE89370400440532013000";

            char[] ibanChars = randomIBAN.ToCharArray();

            for (int i = 0; i < randomIBAN.Length; i++)
            {
                if (ibanChars[i] >= '0' && ibanChars[i] <= '9')
                {
                    int randomDigit = rand.Next(10);
                    ibanChars[i] = (char)('0' + randomDigit);
                }
            }

            return new string(ibanChars);
        }
        public void AddService(bool isCredit, int term, decimal sum, float percentage)
        {
            if (isCredit)
            {
                _services.Add(new Credit(term,percentage,sum));
            }
            else
            {
                _services.Add(new Deposit(term,percentage,sum));
            }
            
        }
        public void CreateCard(bool isCreditCard, List<ICard> cards, string pin)
        {
            if (isCreditCard)
            {
                cards.Add(new CreditCard(1000,DateTime.Now.AddYears(5),pin,this));
            }
            else
            {
                cards.Add(new DebitCard(DateTime.Now.AddYears(6), pin, this));
            }
        }
    }
}
