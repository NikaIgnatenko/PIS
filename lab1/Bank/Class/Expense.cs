using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class Expense:Operation
    {
        public override decimal Sum { get { return _sum>0? decimal.Negate(_sum):_sum; } }
        public Expense(decimal sum,ICard card) : base(sum,card)
        {
        }
        public override string ToString()
        {
            return $"[Expense]\n" +
                   $"Sum: {_sum}\n" +
                   $"{_date}";
        }
    }
}
