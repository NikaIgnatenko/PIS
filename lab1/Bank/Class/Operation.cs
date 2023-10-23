using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal abstract class Operation:IOperation
    {
        protected DateTime _date;
        protected decimal _sum;
        protected ICard _card;
        public DateTime Date { get { return _date; } }
        public abstract decimal Sum {  get; }
        public ICard Card { get { return _card; } }
        public Operation(decimal sum,ICard card)
        {
            _date = DateTime.Now;
            _sum = sum;
            _card = card;
        }
        public abstract override string ToString();
    }
}
