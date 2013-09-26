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
            var r = new Random();

            int yoyoCount,yoyoLen,yoyoX,yoyoY,yoyoTimes,yoyoHangs,fieldLen;
            ConsoleColor yoyoColor = ConsoleColor.Black;

            List<YoYo> myYoYos = new List<YoYo>();

            yoyoCount = r.Next(1, 16);

            fieldLen = (80 / yoyoCount);

            for (int i = 0; i < yoyoCount; i++)
            {
                yoyoColor++;
                yoyoLen = r.Next(2, 16);
                yoyoX = i * fieldLen + r.Next(0, fieldLen + 1);
                yoyoY = r.Next(0, 11);
                yoyoTimes = r.Next(1, 6);
                yoyoHangs = r.Next(0, 6);
                myYoYos.Add(new YoYo(yoyoColor, yoyoLen, yoyoX, yoyoY, yoyoTimes,yoyoHangs));
            }

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
                }
                Thread.Sleep(100);
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
        int hangTime = 0;

        Direction currentDirection = Direction.DOWN;
        int yIdx = -1;
        int pass = 0;
        int hangPass = 0;
        bool done = false;

        public YoYo(ConsoleColor decColor = ConsoleColor.White, int decLength = 10, int decX = 5, int decY = 5, int decTimes = 1, int decHangs = 0)
        {
            color = decColor;
            length = decLength;
            xPos = decX;
            yPos = decY;
            runTime = decTimes;
            hangTime = decHangs;
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

        public int HangTime
        {
            get { return hangTime; }
            set { hangTime = value; }
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
                    if (yIdx != length) yIdx++;

                    if (yIdx == length)
                    {
                        if (hangPass < hangTime)
                        {
                            hangPass++;
                        }
                        else
                        {
                            currentDirection = Direction.UP;
                        }
                    }
                }
                else
                {
                    yIdx--;

                    if (yIdx == 0)
                    {
                        currentDirection = Direction.DOWN;
                        hangPass = 0;
                        pass++;
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
            for (int i = 0; i < yIdx; i++)
            {
                Console.SetCursorPosition(xPos, yPos + i);
                Console.Write("|");
            }
            Console.SetCursorPosition(xPos, yPos + yIdx);
            Console.Write("@");
            Console.SetCursorPosition(xPos, yPos + yIdx + 1);
            Console.Write(" ");
        }

    }
}
