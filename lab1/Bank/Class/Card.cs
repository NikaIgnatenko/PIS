using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Bank.Class
{
    internal abstract class Card:ICard
    {
        protected string _number;
        protected decimal _creditLimit;
        protected decimal _unusedCredit;
        protected DateTime _expDate;
        protected string _cvv;
        protected string _pin;
        protected IAccount _cardHolder;
        public string Number { get { return _number; } }
        public decimal CreditLimit { get {  return _creditLimit; } }
        public decimal UnusedCredit { get { return _unusedCredit; } }
        public DateTime ExpDate { get { return _expDate; } }
        public string CVV { get { return _cvv;} }
        public IAccount CardHolder { get { return _cardHolder; } }
        public string Pin { get { return _pin; } }
        public Card(decimal creditLimit, DateTime expDate, string pin, IAccount cardHolder)
        {
            _number = GenerateCardNumber();
            _creditLimit = creditLimit;
            _unusedCredit = creditLimit;
            _expDate = expDate;
            _cvv = GenerateCVV();
            _pin = pin;
            _cardHolder = cardHolder;
        }
        public abstract event Notification OnOperationAdded;
        static string GenerateCardNumber()
        {
            Random random = new Random();
            string result = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                result += random.Next(10);
            }

            return result;
        }
        static string GenerateCVV()
        {
            Random random = new Random();
            string result = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                result += random.Next(10);
            }

            return result;
        }
        public abstract override string ToString();
        public abstract bool AddOperation(bool isExpense, decimal sum);
    }
}
