using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class Deposit:Service
    {
        public override string ToString()
        {
            return $"[Deposit]\n" +
                   $"Created: {_created}\n" +
                   $"Months: {_term}\n" +
                   $"Percentage: {_percentage}%\n" +
                   $"Sum: {_sum}\n" +
                   $"Finished : {_isFinished}";
        }
        public Deposit(int term, float percentage, decimal sum) : base(term, sum, percentage)
        {
        }
    }
}
