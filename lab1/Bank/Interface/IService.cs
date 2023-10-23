using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Interface
{
    internal interface IService
    {
        DateTime Created { get; }
        int Term {  get; }
        decimal Sum {  get; }
        float Percentage {  get; }
        bool IsFinished {  get; }
    }
}
