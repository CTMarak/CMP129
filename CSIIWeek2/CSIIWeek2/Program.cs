using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSIIWeek2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MAX = 100;

            string[] colorName = ConsoleColor.GetNames(typeof(ConsoleColor));

            int colorPtr = 0; int ctr = 0;
            ConsoleColor color = ConsoleColor.Black;

            Console.BackgroundColor = ConsoleColor.Black;

            for (ctr = 0; ctr < MAX; ctr++)
            {
                if (ctr % 10 == 0)
                {
                    do
                    {
                        color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName[colorPtr]);
                        colorPtr++;
                    } while (color == ConsoleColor.Black || color == ConsoleColor.White);

                    Console.ForegroundColor = color;
                }

                Console.WriteLine(ctr+1);
            }

            Console.ForegroundColor = ConsoleColor.White;

            for (ctr = 0; ctr < MAX; ctr++)
            {
                if ((ctr + 1) % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.WriteLine(ctr + 1);
            }

        }
    }
}
