using Bank.Interface;
using System;

namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager.Start();
        }
        public static void OtherMethod(IOperation operation,ICard card)
        {
            if(operation == null)
            {
                Console.WriteLine($"[FAILURE]\nOperation from this card\n{card}");
            }
            else
            {
                Console.WriteLine($"Operation-> {operation}\nFrom-> {card}");
            }
        }
    }
}
