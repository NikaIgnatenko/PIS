using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal abstract class Service:IService
    {
        protected DateTime _created;
        protected int _term;
        protected decimal _sum;
        protected float _percentage;
        protected bool _isFinished;
        public DateTime Created { get { return _created; } }
        public int Term { get { return _term; } }
        public decimal Sum { get { return _sum; } }
        public float Percentage { get { return _percentage; } }
        public bool IsFinished { get { return _isFinished; } }
        public Service(int term, decimal sum, float percentage)
        {
            _created = DateTime.Now;
            _term = term;
            _sum = sum;
            _percentage = percentage;
            _isFinished = false;
        }
        public abstract override string ToString();
    }
}
