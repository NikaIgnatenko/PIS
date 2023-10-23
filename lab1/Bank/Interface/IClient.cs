using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Interface
{
    internal interface IClient
    {
        string Name {  get; }
        string LastName {  get; }
        string Number {  get; }
        string Address {  get; }
        string IPN {  get; }
    }
}
