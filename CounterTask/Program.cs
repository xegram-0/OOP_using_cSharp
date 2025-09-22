using System;
using CounterTask;

namespace CounterTask
{
    internal class Program
    {
        private static void PrintCounters(Counter[] counters)
        {
            foreach (var counter in counters)
            {
                Console.WriteLine("{0} is {1}", counter.Name, counter.Ticks);
            }
        }
        static void Main(string[] args)
        {
            Counter[] myCounters = new Counter[4];
            myCounters[0] = new Counter("CounterTask 1");
            myCounters[1] = new Counter("CounterTask 2");
            myCounters[2] = myCounters[0];
            for (int i = 1; i < 9; i++)
            {
                myCounters[0].Increment();
            }
            for (int i = 1; i < 14; i++)
            {
                myCounters[1].Increment();
                
                /*
                Testing ResetByDefault()
                
                Console.WriteLine(myCounters[1].Ticks);
                myCounters[1].ResetByDefault();
                */
            }
            PrintCounters(myCounters);
            //myCounters[2].Reset();
            myCounters[2].ResetByDefault(); //Stop overflow
            PrintCounters(myCounters);
            
            /*
                Checked and unchecked problem
                When using checked keyword the program with thrown the exception message if the value triggers overflow.
                While unchecked can just ignore what happens without crashing the program.
                Similar to exception and catch in python.
                
                For this instance, the value created overflow resulted in bug/crash.
                By using checked or unchecked, the problem would be identified in the terminal.
             */

            myCounters[3] = new Counter("CounterTask 3");
            for (int i = 1; i <= 100; i++)
            {
                myCounters[3].Increment();
                //PrintCounters(myCounters);
            }
        }
    }
}