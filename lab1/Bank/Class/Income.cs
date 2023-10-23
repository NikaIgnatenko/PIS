using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class Income : Operation
    {
        public override decimal Sum { get { return _sum<0?decimal.Negate(_sum):_sum; } }
        public Income(decimal sum,ICard card) : base(sum, card)
        {
        }
        public override string ToString()
        {
            return $"[Income]\n" +
                   $"Sum: {_sum}\n" +
                   $"{_date}";
        }
    }
}
