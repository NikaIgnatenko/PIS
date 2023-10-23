using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Interface
{
    delegate void Notification(IOperation operation,ICard card);
    internal interface ICard
    {
        string Number { get; }
        decimal CreditLimit { get; }
        decimal UnusedCredit {  get; }
        DateTime ExpDate { get; }
        string CVV { get; }
        string Pin {  get; }
        IAccount CardHolder { get; }
        event Notification OnOperationAdded;
        bool AddOperation(bool isExpense, decimal sum);
    }
}
