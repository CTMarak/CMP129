using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleYoYo
{
    class ConsoleYoYo
    {
        static void Main(string[] args)
        {
            List<YoYo> myYoYos = new List<YoYo>();
            myYoYos.Add(new YoYo(ConsoleColor.Magenta, 8, 4, 7, 2));
            myYoYos.Add(new YoYo(ConsoleColor.Yellow, 15, 15, 1, 1));
            myYoYos.Add(new YoYo(ConsoleColor.Green, 12, 7, 6, 1));
            myYoYos.Add(new YoYo(ConsoleColor.White, 6, 13, 13, 3));
            myYoYos.Add(new YoYo(ConsoleColor.Blue, 7, 6, 3, 2));

            Console.Clear();

            bool allDone;

            do
            {
                allDone = true;
                foreach (YoYo yoyo in myYoYos)
                {
                    yoyo.UpdateYoYo();
                    yoyo.DrawYoYo();
                    allDone = (allDone && yoyo.Done);
                    Thread.Sleep(100);
                }
            } while (!allDone);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0,24);

        }
    }

    public enum Direction
    {
        DOWN=0,
        UP,
    }

    public class YoYo
    {
        ConsoleColor color = ConsoleColor.White;
        int length = 10;
        int xPos = 5;
        int yPos = 5;
        int runTime = 1;

        Direction currentDirection = Direction.DOWN;
        int yIdx = 0;
        int pass = 0;
        bool done = false;

        public YoYo(ConsoleColor decColor = ConsoleColor.White, int decLength = 10, int decX = 5, int decY = 5, int decTimes = 1)
        {
            color = decColor;
            length = decLength;
            xPos = decX;
            yPos = decY;
            runTime = decTimes;
        }

        public bool Done
        {
            get { return done; }
        }

        public int RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public int YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public Direction CurrentDirection
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }

        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public void UpdateYoYo()
        {
            if (!done)
            {
                if (currentDirection == Direction.DOWN)
                {
                    yIdx += 1;
                    if (yIdx == length - 1)
                    {
                        currentDirection = Direction.UP;
                    }
                }
                else
                {
                    yIdx -= 1;
                    if (yIdx == 0)
                    {
                        currentDirection = Direction.DOWN;
                        pass += 1;
                        if (pass == runTime)
                        {
                            done = true;
                        }
                    }
                }
            }
        }

        public void DrawYoYo()
        {
            Console.ForegroundColor = color;
            if (yIdx == 0)
            {
                Console.SetCursorPosition(xPos, yPos + yIdx - 1);
                Console.Write(" ");
                Console.SetCursorPosition(xPos, yPos);
                Console.Write("@");
                Console.SetCursorPosition(xPos, yPos + yIdx + 1);
                Console.Write(" ");
            }
            else
            {
                if (currentDirection == Direction.UP)
                {
                    Console.SetCursorPosition(xPos, yPos + yIdx - 1);
                    Console.Write("|");
                    Console.SetCursorPosition(xPos, yPos + yIdx);
                    Console.Write("@");
                    Console.SetCursorPosition(xPos, yPos + yIdx + 1);
                    Console.Write(" ");
                }
                else
                {
                    Console.SetCursorPosition(xPos, yPos + yIdx - 1);
                    Console.Write("|");
                    Console.SetCursorPosition(xPos, yPos + yIdx);
                    Console.Write("@");
                    Console.SetCursorPosition(xPos, yPos + yIdx + 1);
                    Console.Write(" ");
                }
            }
        }

    }
}
