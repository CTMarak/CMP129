using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            int total = 0;

            //Find the sum of all the multiples of 3 or 5 below 1000.
            Console.WriteLine("1. Find the sum of all the multiples of 3 or 5 below 1000.");
            total = 0;
            for (int i = 3; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    total += i;
                }
            }
            Console.WriteLine("   The sum of all multiples of 3 or 5 below 1000 is " + total.ToString());

            Console.WriteLine();

            //By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
            Console.WriteLine("2. What is the sum of all even Fibonacci numbers less than four million?");
            total = 0;
            int f1 = 0;
            int f2 = 1;
            int fn = 0;
            int biggest = 4000000;
            do
            {
                fn = f1 + f2;
                if (fn % 2 == 0) total += fn;
                f1 = f2;
                f2 = fn;
            } while (fn < biggest);
            Console.WriteLine("   The sum of all even Fibonacci numbers less than four million is " + total.ToString());

            Console.WriteLine();

            //what is the largest prime factor of 600851475143
            Console.WriteLine("3. What is the largest prime factor of 600851475143?");
            Int64 Number = 600851475143;
            Int64 Largest = 0;
            if (IsPrime(Number))
            {
                Console.WriteLine("   The largest prime factor of " + Number.ToString() + " is " + Number.ToString() + ".");
            }
            else
            {
                for (Int64 i = 2; i < Number; i++ )
                {
                    if (i * i > Number) break;
                    if (Number % i == 0)
                    {
                        if (IsPrime(i))
                        {
                            Largest = i;
                            Console.WriteLine("   A prime factor of " + Number.ToString() + " is " + i.ToString() + ".");
                        }
                    }
                }
                Console.WriteLine("   The largest prime factor of " + Number.ToString() + " is " + Largest.ToString() + ".");
            }

            Console.WriteLine();

            //What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
            Console.WriteLine("5. What is the smallest positive number that is evenly divisible");
            Console.WriteLine("   by all of the numbers from 1 to 20?");
            int benchmark = 1;
            int smallest = 20;
            do
            {
                smallest += 1;
                benchmark = 0;
                for (int i = 1; i <= 20; i++)
                {
                    benchmark += (smallest % i);
                }
            } while (benchmark != 0);
            Console.WriteLine("   The smallest positive number that is evenly divisible by all of");
            Console.WriteLine("   the numbers from 1 to 20 is " + smallest.ToString() + ".");

            Console.WriteLine();

            //what is the 10001st prime number?
            Console.WriteLine("7. What is the 10001st prime number?");
            int PrimeCounter = 0;
            Int64 numberCounter = 0;
            Int64 finalPrime = 0;
            while (PrimeCounter < 10001)
            {
                numberCounter += 1;
                if (IsPrime(numberCounter))
                {
                    finalPrime = numberCounter;
                    PrimeCounter += 1;
                }
            }
            Console.WriteLine("   The 10,001st prime is " + finalPrime.ToString());
        }

        static bool IsPrime(Int64 N)
        {
            if (N == 1) return false;
            if (N == 2) return true;
            for (Int64 i = 2; i < N; i++)
            {
                if (i * i > N) return true;
                if (N % i == 0) return false;
            }
            return true;
        }
    }
}
