using CssDupFinder.Commands;
using System;

namespace CssDupFinder
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var arguments = new ArgumentParser(args);
            var command = CommandFactory.CreateInstanceOf(arguments.Command, arguments);

            command.Execute();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}