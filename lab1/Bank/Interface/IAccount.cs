using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Interface
{
    enum Currency
    {
        uah,
        eur,
        usd
    }
    internal interface IAccount
    {
        IClient Client { get; }
        Currency Currency { get; }
        decimal Balance { get; set; }
        List<IService> Services { get; }
        string IBAN { get; }
        List<IOperation> Operations { get; }
        void AddService(bool isCredit, int term, decimal sum, float percentage);
        void CreateCard(bool isCreditCard, List<ICard> cards, string pin);
    }
}
