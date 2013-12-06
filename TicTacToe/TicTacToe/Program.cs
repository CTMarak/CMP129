using System;
using System.Collections.Generic;
using System.Linq; // for Array.Any method
using System.Text;

namespace TicTacToe
{
    class Program
    {

        const int EMPTY = -1;
        const int ME = 0;
        const int YOU = 1;

        static string[][] pieces = new string[][] {
      new string[] { "Me", "X" },     // the program as player
      new string[] { "You", "O" }     // the human player
    };

        // the board holds 0 (for Me) or 1 (for You) or -1 (for EMPTY)
        static int[] board = new int[9];
        static int currentPlayer = EMPTY;
        static int thePlay = EMPTY;
        static int turn = EMPTY;

        // I may need these for my algorithm
        static int[] corners = new int[] { 0, 2, 6, 8 };
        static int[] oppositeCorners = new int[] { 8, 6, 2, 0 };
        static int[] edges = new int[] { 1, 3, 5, 7 };
        static int center = 4;

        // deliniate wins to think ahead and test win conditions ... diagonals last
        static int[][] wins = new int[][] {
      new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 },
      new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 },
      new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 } };

        // deliniate forks that involve each square
        // complicated! - jagged array 4 indices deep
        // index #1 = fork square ... so a number from 0 to 8
        // index #2 = list of fork pairs of pairs of squares - variable size: 1 for edge, 3 for corner, 6 for center
        // index #3 = which pair of pairs of squares - dimension always 2
        // index #4 = which square in pair of squares - dimension always 2
        static int[][][][] forks = new int[9][][][] {
      new int[3][][] { new int[2][] { new int[2] { 1, 2 }, new int[2] { 3, 6 } },
                       new int[2][] { new int[2] { 1, 2 }, new int[2] { 4, 8 } },
                       new int[2][] { new int[2] { 3, 6 }, new int[2] { 4, 8 } } },
      new int[1][][] { new int[2][] { new int[2] { 0, 2 }, new int[2] { 4, 7 } } },
      new int[3][][] { new int[2][] { new int[2] { 0, 1 }, new int[2] { 4, 6 } },
                       new int[2][] { new int[2] { 0, 1 }, new int[2] { 5, 8 } },
                       new int[2][] { new int[2] { 4, 6 }, new int[2] { 5, 8 } } },
      new int[1][][] { new int[2][] { new int[2] { 0, 6 }, new int[2] { 4, 5 } } },
      new int[6][][] { new int[2][] { new int[2] { 0, 8 }, new int[2] { 1, 7 } },
                       new int[2][] { new int[2] { 0, 8 }, new int[2] { 2, 6 } },
                       new int[2][] { new int[2] { 0, 8 }, new int[2] { 3, 5 } },
                       new int[2][] { new int[2] { 1, 7 }, new int[2] { 2, 6 } },
                       new int[2][] { new int[2] { 1, 7 }, new int[2] { 3, 5 } },
                       new int[2][] { new int[2] { 2, 6 }, new int[2] { 3, 5 } } },
      new int[1][][] { new int[2][] { new int[2] { 2, 8 }, new int[2] { 3, 4 } } },
      new int[3][][] { new int[2][] { new int[2] { 0, 3 }, new int[2] { 2, 4 } },
                       new int[2][] { new int[2] { 0, 3 }, new int[2] { 7, 8 } },
                       new int[2][] { new int[2] { 2, 4 }, new int[2] { 7, 8 } } },
      new int[1][][] { new int[2][] { new int[2] { 1, 4 }, new int[2] { 6, 8 } } },
      new int[3][][] { new int[2][] { new int[2] { 0, 4 }, new int[2] { 2, 5 } },
                       new int[2][] { new int[2] { 0, 4 }, new int[2] { 6, 7 } },
                       new int[2][] { new int[2] { 2, 5 }, new int[2] { 6, 7 } } } };

        // games need random stuff, right?
        static Random r = new Random();

        // Begin Main Loop
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tic-Tac-Toe");
                clearBoard();
                displayBoard();
                currentPlayer = r.Next(0, 2);  // 0 or 1 - so an index for pieces ... ME or YOU
                Console.WriteLine("The first play goes to {0} - playing {1}s.", playerName(currentPlayer), playerXO(currentPlayer));
                Console.WriteLine();

                turn = -1;
                while (true)
                {
                    turn += 1;
                    thePlay = getPlay(currentPlayer);
                    makePlay(thePlay, currentPlayer);
                    displayBoard();
                    if (gameIsWon())
                    {
                        Console.WriteLine("We have a winner: {0}!", playerName(currentPlayer));
                        break;
                    }
                    else if (gameIsTied())
                    {
                        Console.WriteLine("Cat game ... as usual.");
                        break;
                    }
                    currentPlayer = switchPlayers(currentPlayer);
                }

                if (!playAgain())
                {
                    return; // we are done here
                }
            }
        }
        // End Main Loop


        // functions and such

        static void clearBoard()
        {
            for (int i = 0; i < board.Length; i++)
                board[i] = EMPTY;
        }

        static string playerName(int player)
        {
            return pieces[player][0];
        }

        static string playerXO(int player)
        {
            return pieces[player][1];
        }

        static int switchPlayers(int player)
        {
            return (player + 1) % 2;
        }

        static void displayBoard()
        {
            Console.WriteLine(" {0} | {1} | {2}", xOAt(6), xOAt(7), xOAt(8));
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2}", xOAt(3), xOAt(4), xOAt(5));
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2}", xOAt(0), xOAt(1), xOAt(2));
            Console.WriteLine();
        }

        static string xOAt(int square)
        {
            if (board[square] == EMPTY)
            {
                return (square + 1).ToString();  // should display square's number if empty
            }
            return playerXO(board[square]);
        }

        static bool playAgain()
        {
            Console.WriteLine();
            Console.WriteLine("Play again? [Y/N]");
            while (true)
            {
                ConsoleKeyInfo result = Console.ReadKey(false);
                if (result.KeyChar == 'Y' || result.KeyChar == 'y')
                {
                    return true;
                }
                else if (result.KeyChar == 'N' || result.KeyChar == 'n')
                {
                    return false;
                }
            }
        }

        // - Logic to pick play

        static int getPlay(int player)
        {
            if (player == YOU)
            {
                return askForPlay();
            }
            else
            {
                int myPlay = decidePlay(player);
                Console.WriteLine("I select square {0}.", myPlay + 1);
                return myPlay;
            }
        }

        static void makePlay(int square, int player)
        {
            board[square] = player;
        }

        static void takeBackPlay(int square)
        {
            board[square] = EMPTY;
        }

        static bool gameIsWon()
        {
            return wins.Any(win => threeInRow(win[0], win[1], win[2]));
        }

        static bool threeInRow(int a1, int a2, int a3)
        {
            return board[a1] != EMPTY && board[a1] == board[a2] && board[a1] == board[a3];
        }

        static bool gameIsTied()
        {
            return !board.Any(square => square == EMPTY);
        }

        static int askForPlay()
        {
            while (true)
            {
                Console.Write("Enter your play [1-9]: ");
                ConsoleKeyInfo playKey = Console.ReadKey();
                Console.WriteLine();
                if ((playKey.Key >= ConsoleKey.D1 && playKey.Key <= ConsoleKey.D9) || (playKey.Key >= ConsoleKey.NumPad1 && playKey.Key <= ConsoleKey.NumPad9))
                {
                    int play = playKey.KeyChar - '1';
                    if (board[play] == EMPTY)
                    {
                        return play;
                    }
                    else
                    {
                        Console.WriteLine("Square {0} is already taken. Make a different play.", play + 1);
                    }
                }
                else
                {
                    Console.WriteLine("Must be a number: 1-9.");
                    Console.WriteLine();
                }
            }
        }

        static int decidePlay(int player)
        {
            // the square will be chosen as the first of these that works
            int square = EMPTY; // we don't have anything yet
            // do I have the first play? if so pick the bottom left corner
            if (turn == 0)
            {
                return 0; // 1 - 1 = 0
            }
            // if I can win, I should make that play
            if (winningPlay(player, out square))
            {
                return square;
            }
            // if I can block my opponent's win, I should do that
            if (blockingPlay(player, out square))
            {
                return square;
            }
            // if I can create a fork, I should do that
            if (forkPlay(player, out square))
            {
                return square;
            }
            // if I can force my opponent to make a non-fork play, I should do that
            // in other words, I will set up two in a row that my opponent has to block
            // so long as blocking my win will not set up a fork for my opponent
            if (forceNonForkPlay(player, out square))
            {
                return square;
            }
            // if I can block my opponent from creating a fork, I should do that
            if (blockForkPlay(player, out square))
            {
                return square;
            }
            // if the center is free, I should take it
            // either I wasn't first or my opponent is stupid
            if (board[center] == EMPTY)
            {
                return center;
            }
            // if I can take a corner opposite one of my opponents, I should do that
            // this could possibly setup a future fork
            if (oppositeCornerPlay(player, out square))
            {
                return square;
            }
            // if I can take an unoccupied corner, I should
            // corners are usually better than edges - just look at the forks array
            if (emptyCornerPlay(player, out square))
            {
                return square;
            }
            // oh well, I'm left with edges - probably going to be a CAT game
            if (emptyEdgePlay(player, out square))
            {
                return square;
            }
            // really not needed, but it makes the compiler happy
            // between center, emptyCorner and emptyEdge I really cannot get here
            return square;
        }

        static bool winningPlay(int player, out int square)
        {
            square = EMPTY;
            foreach (var win in wins)
            {
                if (playerHasTwo(player, win, out square))
                {
                    return true;
                }
            }
            return false;
        }

        static bool blockingPlay(int player, out int square)
        {
            square = EMPTY;
            int opponent = switchPlayers(player);
            foreach (var win in wins)
            {
                if (playerHasTwo(opponent, win, out square))
                {
                    return true;
                }
            }
            return false;
        }

        static bool forkPlay(int player, out int square)
        {
            square = EMPTY;
            int opponent = switchPlayers(player);
            for (square = 0; square < board.Length; square++)
            {
                if (board[square] == EMPTY)
                {
                    for (int i = 0; i < forks[square].Length; i++)
                    {
                        if (playerHasOne(player, forks[square][i][0]) && playerHasOne(player, forks[square][i][1]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool forceNonForkPlay(int player, out int square)
        {
            square = EMPTY;
            int heldSquare = EMPTY;
            int oppSquare = EMPTY;
            int trySquare = EMPTY;
            bool bad;
            int opponent = switchPlayers(player);
            foreach (var win in wins)
            {
                if (!onlyPlayerHasOne(player, win, out heldSquare))
                    continue;
                for (int i = 0; i < 3; i++)
                {
                    trySquare = win[i];
                    if (trySquare == heldSquare)
                        continue;
                    makePlay(trySquare, player);
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == i)
                            continue;
                        oppSquare = win[j];
                        if (oppSquare == heldSquare)
                            continue;
                        bad = false;
                        for (int k = 0; k < forks[oppSquare].Length; k++)
                        {
                            if (playerHasOne(opponent, forks[oppSquare][k][0]) && playerHasOne(opponent, forks[oppSquare][k][1]))
                            {
                                bad = true;
                                break;
                            }
                        }
                        if (!bad)
                        {
                            square = trySquare;
                        }
                    }
                    takeBackPlay(trySquare);
                    if (square != EMPTY)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool blockForkPlay(int player, out int square)
        {
            square = EMPTY;
            int opponent = switchPlayers(player);
            for (square = 0; square < board.Length; square++)
            {
                if (board[square] == EMPTY)
                {
                    for (int i = 0; i < forks[square].Length; i++)
                    {
                        if (playerHasOne(opponent, forks[square][i][0]) && playerHasOne(opponent, forks[square][i][1]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool oppositeCornerPlay(int player, out int square)
        {
            square = EMPTY;
            int opponent = switchPlayers(player);
            for (int i = 0; i < corners.Length; i++)
            {
                square = corners[i];
                if (board[square] == EMPTY && board[oppositeCorners[i]] == opponent)
                {
                    return true;
                }
            }
            return false;
        }

        static bool emptyCornerPlay(int player, out int square)
        {
            square = EMPTY;
            foreach (int trySquare in corners)
            {
                if (board[trySquare] == EMPTY)
                {
                    square = trySquare;
                    return true;
                }
            }
            return false;
        }

        static bool emptyEdgePlay(int player, out int square)
        {
            square = EMPTY;
            foreach (int trySquare in edges)
            {
                if (board[trySquare] == EMPTY)
                {
                    return true;
                }
            }
            return false;
        }

        static bool playerHasOne(int player, int[] pair)
        {
            bool found = false;
            foreach (int square in pair)
            {
                if (board[square] == player)
                {
                    found = true;
                }
                else if (board[square] != EMPTY)
                {
                    return false;
                }
            }
            return found;
        }

        static bool playerHasTwo(int player, int[] win, out int square)
        {
            int cnt = 0;
            square = int.MinValue;
            foreach (int trySquare in win)
            {
                if (board[trySquare] == player)
                {
                    cnt++;
                }
                else if (board[trySquare] == EMPTY)
                {
                    square = trySquare;
                }
            }
            return (cnt == 2 && square >= 0);
        }

        static bool onlyPlayerHasOne(int player, int[] win, out int square)
        {
            int cnt = 0;
            square = EMPTY;
            foreach (int trySquare in win)
            {
                if (board[trySquare] == player)
                {
                    square = trySquare;
                    cnt++;
                }
                else if (board[trySquare] != EMPTY)
                {
                    return false;
                }
            }
            return (cnt == 1);
        }
    }

}
