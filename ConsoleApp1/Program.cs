using System;

namespace ConsoleApp1
{
    internal class Program
    {
        public static Tuple<string,int> Hello()
        {
            return new Tuple<string,int>("hello", 123);
        }
        static void Main(string[] args)
        {
            var x = Hello();
            Console.WriteLine(x.Item1 + x.Item2);
        }
    }
}
