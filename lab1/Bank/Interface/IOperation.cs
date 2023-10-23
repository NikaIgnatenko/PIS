using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Interface
{
    internal interface IOperation
    {
        decimal Sum {  get; }
        DateTime Date { get; }
        ICard Card { get; }
    }
}
