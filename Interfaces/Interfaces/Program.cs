using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    class Program
    {
        public interface IComputable
        {
            string Problem { get; }
            string AnswerDesc { get; }
            Int64 Answer { get; }
        }
        static void Main(string[] args)
        {
            List<IComputable> computables = new List<IComputable>();
            computables.Add(new Problem1());
            computables.Add(new Problem2());
            computables.Add(new Problem3());
            computables.Add(new Problem5a());
//            computables.Add(new Problem5());
            computables.Add(new Problem7());

            foreach (IComputable computable in computables)
            {
                Toolset.DisplayAnswer(computable);
            }
        }

        public class Problem1 : IComputable
        {
            public Int64 Answer
            {
                get
                {
                    Int64 total = 0;
                    for (Int64 i = 3; i < 1000; i++)
                    {
                        if (i % 3 == 0 || i % 5 == 0)
                        {
                            total += i;
                        }
                    }
                    return total;
                }
            }

            public string Problem { get { return "1. Find the sum of all the multiples of 3 or 5 below 1000."; } }
            public string AnswerDesc { get { return "   The sum of all the multiples of 3 or 5 below 1000 is"; } }
        }

        public class Problem2 : IComputable
        {
            public Int64 Answer
            {
                get
                {
                    Int64 total = 0;
                    Int64 f1 = 0;
                    Int64 f2 = 1;
                    Int64 fn = 0;
                    Int64 biggest = 4000000;
                    do
                    {
                        fn = f1 + f2;
                        if (fn % 2 == 0) total += fn;
                        f1 = f2;
                        f2 = fn;
                    } while (fn < biggest);
                    return total;
                }
            }

            public string Problem    { get { return "2. What is the sum of all even Fibonacci numbers less than four million?"; } }
            public string AnswerDesc { get { return "   The sum of all even Fibonacci numbers less than four million is"; } }
        }

        public class Problem3 : IComputable
        {
            public Int64 Answer
            {
                get
                {
                    Int64 Number = 600851475143;
                    Int64 Largest = 0;
                    if (Toolset.IsPrime(Number))
                    {
                        return Number;
                    }
                    else
                    {
                        for (Int64 i = 2; i < Number; i++ )
                        {
                            if (i * i > Number) break;
                            if (Number % i == 0)
                            {
                                if (Toolset.IsPrime(i))
                                {
                                    Largest = i;
                                }
                            }
                        }
                    }
                    return Largest;
                }
            }

            public string Problem    { get { return "3. What is the largest prime factor of 600851475143?"; } }
            public string AnswerDesc { get { return "   The largest prime factor of 600851475143 is"; } }
        }

        public class Problem5 : IComputable
        {
            public Int64 Answer
            {
                get
                {
                    Int64 benchmark = 1;
                    Int64 smallest = 20;
                    do
                    {
                        smallest += 1;
                        benchmark = 0;
                        for (Int64 i = 1; i <= 20; i++)
                        {
                            benchmark += (smallest % i);
                        }
                    } while (benchmark != 0);
                    return smallest;
                }
            }

            public string Problem    { get { return "5. What is the smallest no. > 0 divisible by all nos. 1 to 20?"; } }
            public string AnswerDesc { get { return "   The smallest no. > 0 divisible by all nos. 1 to 20 is"; } }
       }

        public class Problem5a : IComputable
        {
            const Int64 LIMIT = 42;
            public Int64 Answer
            {
                get
                {
                    Int64 smallest = 1;
                    int primeCt = 0;
                    Int64[] AllPrimes = new Int64[LIMIT];
                    int[] AllExponents = new int[LIMIT];
                    int[] CurExponents = new int[LIMIT];
                    for (Int64 i = 2; i <= LIMIT; i++)
                    {
                        if ( Toolset.IsPrime(i) )
                        {
                            AllPrimes[primeCt] = i;
                            AllExponents[primeCt] = 0;
                            primeCt += 1;
                        }
                    }
                    for (Int64 i = 2; i <= LIMIT; i++)
                    {
                        Int64 dividend = i;
                        for (int j = 0; j < primeCt; j++)
                        {
                            CurExponents[j] = 0;
                            while (dividend % AllPrimes[j] == 0)
                            {
                                CurExponents[j] += 1;
                                if (CurExponents[j] > AllExponents[j])
                                {
                                    smallest = smallest * AllPrimes[j];
                                    AllExponents[j] = CurExponents[j];
                                }
                                dividend = dividend / AllPrimes[j];
                            }
                        }
                    }
                    return smallest;
                }
            }
            public string Problem { get { return "5. What is the smallest no. > 0 divisible by all nos. 1 to " + LIMIT.ToString() + "?"; } }
            public string AnswerDesc { get { return "   The smallest no. > 0 divisible by all nos. 1 to " + LIMIT.ToString() + " is"; } }
        }

        public class Problem7 : IComputable
        {
            public Int64 Answer
            {
                get
                {
                    Int64 PrimeCounter = 0;
                    Int64 numberCounter = 0;
                    Int64 finalPrime = 0;
                    while (PrimeCounter < 10001)
                    {
                        numberCounter += 1;
                        if (Toolset.IsPrime(numberCounter))
                        {
                            finalPrime = numberCounter;
                            PrimeCounter += 1;
                        }
                    }
                    return finalPrime;
                }
            }

            public string Problem    { get { return "7. What is the 10,001st prime number?"; } }
            public string AnswerDesc { get { return "   The 10,001st prime is"; } }
        }

        public static class Toolset
        {
            public static bool IsPrime(Int64 N)
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

            public static void DisplayAnswer (IComputable computable)
            {
                Console.WriteLine(computable.Problem);
                Console.WriteLine(computable.AnswerDesc + " " + computable.Answer.ToString());
                Console.WriteLine();
            }
        }
    }
}