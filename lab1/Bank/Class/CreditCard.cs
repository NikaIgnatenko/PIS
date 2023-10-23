using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class CreditCard:Card
    {
        public CreditCard(decimal creditLimit, DateTime expDate, string pin, IAccount cardHolder):base(creditLimit, expDate, pin, cardHolder)
        {
        }
        public override event Notification OnOperationAdded;
        public override string ToString()
        {
            return $"[Credit Card]\n" +
                   $"{_number}\n" +
                   $"Cred.Limit: {_creditLimit}\n" +
                   $"Unused limit: {_unusedCredit}\n"+
                   $"{_expDate.Month}/{_expDate.Year}\n" +
                   $"CVV: {_cvv}";
        }
        public override bool AddOperation(bool isExpense, decimal sum)
        {
            if(isExpense)
            {
                if (sum > CardHolder.Balance + _unusedCredit)
                {
                    OnOperationAdded?.Invoke(null,this);
                    return false;
                }
                else if (sum <= CardHolder.Balance)
                {
                    Expense expense = new Expense(sum,this);
                    CardHolder.Operations.Add(expense);
                    CardHolder.Balance += expense.Sum;
                    OnOperationAdded?.Invoke(expense,this);
                    return true;
                }
                else if (sum <= CardHolder.Balance + _unusedCredit)
                {
                    Expense expense = new Expense(sum,this);
                    CardHolder.Operations.Add(expense);
                    sum = decimal.Negate(expense.Sum) - _cardHolder.Balance;
                    _cardHolder.Balance = 0;
                    _unusedCredit -= sum;
                    OnOperationAdded?.Invoke(expense,this);
                    return true;
                }
                else { throw new ArgumentOutOfRangeException("Expense error");}
            }
            else
            {
                if(_unusedCredit == _creditLimit)
                {
                    Income income = new Income(sum,this);
                    CardHolder.Operations.Add(income);
                    _cardHolder.Balance += income.Sum;
                    OnOperationAdded?.Invoke(income,this);
                    return true;
                }
                else if(_unusedCredit < CreditLimit) 
                {
                    decimal delta = CreditLimit - UnusedCredit;
                    Income income = new Income(sum, this);
                    CardHolder.Operations.Add(income);
                    sum = income.Sum;
                    if (sum <= delta)
                    {
                        _unusedCredit += sum;
                        OnOperationAdded?.Invoke(income,this);
                        return true;
                    }
                    else
                    {
                        _unusedCredit += delta;
                        sum -= delta;
                        _cardHolder.Balance += sum;
                        OnOperationAdded?.Invoke(income, this);
                        return true;
                    }
                }
                else { throw new ArgumentOutOfRangeException("Income error"); }
            }
        }
    }
}
