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

            int yoyoPairs,yoyoLen,yoyoX,yoyoY,yoyoTimes,yoyoHangs,fieldLen;
            ConsoleColor yoyoColor = ConsoleColor.Black;

            List<YoYo> myYoYos = new List<YoYo>();

            yoyoPairs = r.Next(1, 8);

            fieldLen = (80 / (2 * yoyoPairs));

            for (int i = 0; i < yoyoPairs; i++)
            {
                yoyoColor++;
                yoyoLen = r.Next(2, 16);
                yoyoX = 2 * i * fieldLen + r.Next(0, fieldLen) + 1;
                yoyoY = r.Next(0, 11);
                yoyoTimes = r.Next(1, 6);
                myYoYos.Add(new YoYo(yoyoColor, yoyoLen, yoyoX, yoyoY, yoyoTimes));

                yoyoColor++;
                yoyoLen = r.Next(2, 16);
                yoyoX = (2 * i + 1) * fieldLen + r.Next(0, fieldLen) + 1;
                yoyoY = r.Next(0, 11);
                yoyoTimes = r.Next(1, 6);
                yoyoHangs = r.Next(2, 6);
                myYoYos.Add(new TrickYoYo(yoyoHangs, yoyoColor, yoyoLen, yoyoX, yoyoY, yoyoTimes));
            }

            //myYoYos.Add(new YoYo(++yoyoColor, 5, 1, 5, 3));
            //myYoYos.Add(new YoYo(++yoyoColor, 5, 21, 5, 3));
            //myYoYos.Add(new TrickYoYo(5, ++yoyoColor, 5, 41, 5, 3));
            //myYoYos.Add(new TrickYoYo(5, ++yoyoColor, 5, 61, 5, 3));

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
                Thread.Sleep(200);
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
        protected int length = 10;
        int xPos = 5;
        int yPos = 5;
        protected int runTime = 1;
        char body = '0';

        protected Direction currentDirection = Direction.DOWN;
        protected int yIdx = -1;
        protected int pass = 0;
        protected bool done = false;

        public YoYo(ConsoleColor decColor = ConsoleColor.White, int decLength = 10, int decX = 5, int decY = 5, int decTimes = 1, char decBody = '0')
        {
            color = decColor;
            length = decLength;
            xPos = decX;
            yPos = decY;
            runTime = decTimes;
            body = decBody;
        }

        public char Body
        {
            get { return body; }
            set { body = value; }
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

        public virtual void UpdateYoYo()
        {
            if (!done)
            {
                if (currentDirection == Direction.DOWN)
                {
                    if (yIdx != length) yIdx++;

                    if (yIdx == length)
                    {
                        currentDirection = Direction.UP;
                    }
                }
                else
                {
                    yIdx--;

                    if (yIdx == 0)
                    {
                        currentDirection = Direction.DOWN;
                        pass++;
                        if (pass == runTime)
                        {
                            done = true;
                        }
                    }
                }
            }
        }

        public virtual void DrawYoYo()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < yIdx; i++)
            {
                Console.SetCursorPosition(xPos, yPos + i);
                Console.Write("|");
            }
            Console.SetCursorPosition(xPos, yPos + yIdx);
            Console.Write(body);
            Console.SetCursorPosition(xPos, yPos + yIdx + 1);
            Console.Write(" ");
        }

    }

    public class TrickYoYo : YoYo
    {
        int hangTime = 0;
        protected int hangPass = 0;

        public TrickYoYo(int decHangs = 0, ConsoleColor decColor = ConsoleColor.White, int decLength = 10, int decX = 5, int decY = 5, int decTimes = 1, char decBody = '@')
            :base(decColor, decLength, decX, decY, decTimes, decBody)
        {
            hangTime = decHangs;
        }

        public int HangTime
        {
            get { return hangTime; }
            set { hangTime = value; }
        }

        public override void UpdateYoYo()
        {
            if (yIdx == length)
            {
                if (hangPass < hangTime)
                {
                    hangPass++;
                    return;
                }
                else
                {
                    hangPass = 0;
                }
            }
            base.UpdateYoYo();
        }

    }
}
