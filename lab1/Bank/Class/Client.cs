using Bank.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Class
{
    internal class Client:IClient
    {
        string _name;
        string _lastName;
        string _number;
        string _address;
        string _ipn;
        public string Name {  get { return _name; } }
        public string LastName { get { return _lastName; } }
        public string Address { get { return _address; } }
        public string Number { get { return _number; } }
        public string IPN { get { return _ipn; } }
        public Client(string name, string lastName, string number, string address, string ipn)
        {
            _name = name;
            _lastName = lastName;
            _number = number;
            _address = address;
            _ipn = ipn;
        }
    }
}
