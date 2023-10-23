using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class DebitCard:Card
    {
        public DebitCard(DateTime expDate, string pin, IAccount cardHolder) : base(0, expDate, pin, cardHolder)
        {
        }
        public override event Notification OnOperationAdded;
        public override string ToString()
        {
            return $"[Debit Card]\n" +
                   $"{_number}\n" +
                   $"{_expDate.Month}/{_expDate.Year}\n" +
                   $"CVV: {_cvv}";
        }
        public override bool AddOperation(bool isExpense, decimal sum)
        {
            if (isExpense)
            {
                if (sum > CardHolder.Balance)
                {
                    OnOperationAdded?.Invoke(null, this);
                    return false;
                }
                else if (sum <= CardHolder.Balance)
                {
                    Expense expense = new Expense(sum,this);
                    
                    CardHolder.Balance += expense.Sum;
                    CardHolder.Operations.Add(expense);
                    OnOperationAdded?.Invoke(expense, this);
                    return true;
                }
                else { throw new ArgumentOutOfRangeException("Expense error"); }
            }
            else
            {
                Income income = new Income(sum, this);
                CardHolder.Operations.Add(income);
                _cardHolder.Balance += income.Sum;
                OnOperationAdded?.Invoke(income, this);
                return true;
            }
        }
    }
}
