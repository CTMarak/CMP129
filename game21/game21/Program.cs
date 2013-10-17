using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game21
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();

            int playerScore = 0;
            int computerScore = 0;
            int roll = 0;
            bool playerDone = false;
            bool computerDone = false;
            bool playerWins = false;
            bool computerWins = false;
            char ans = ' ';

            do
            {
                Console.WriteLine("Your Score is " + playerScore.ToString() + ". Your opponent\'s score is " + computerScore.ToString());
                if (playerDone)
                {
                    Console.WriteLine("You are standing pat.");
                }
                else
                {
                   do
                    {
                        Console.WriteLine("Do you want to roll again?");
                        ans = Console.ReadLine()[0];
                    } while (ans != 'y' && ans != 'Y' && ans != 'n' && ans != 'N');
                    if (ans == 'y' || ans == 'Y')
                    {
                        roll = r.Next(1, 7);
                        Console.WriteLine("You rolled " + roll.ToString() + "!");
                        playerScore += roll;
                        if (playerScore == 21 || (playerScore > computerScore && computerDone))
                        {
                            playerWins = true;
                        }
                        else if (playerScore > 21)
                        {
                            computerWins = true;
                        }
                    }
                    else
                    {
                        playerDone = true;
                    }
                }
                if (!computerDone && !playerWins && !computerWins)
                {
                    if (computerScore <= 17 || playerScore > computerScore)
                    {
                        Console.WriteLine("Opponent elects to roll");
                        roll = r.Next(1, 7);
                        Console.WriteLine("Opponent rolled " + roll.ToString() + "!");
                        computerScore += roll;
                        if (computerScore == 21 || (computerScore > playerScore && playerDone))
                        {
                            computerWins = true;
                        }
                        else if (computerScore > 21)
                        {
                            playerWins = true;
                        }
                    }
                    else
                    {
                        computerDone = true;
                        Console.WriteLine("Opponent elects to stand pat.");
                    }
                }
            } while (!playerWins && !computerWins);

            Console.WriteLine("Final Scores ... You: " + playerScore.ToString() + ", Opponent: " + computerScore.ToString());
            if (playerWins)
            {
                Console.WriteLine("You win!!!!");
            }
            else
            {
                Console.WriteLine("Opponent wins...");
            }
        }
    }
}
