using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
namespace ConsoleApplication1
{
    public interface INode
    {
        void Output();
    }
    public static class Program
    {
        public static void Main()
        {
            EventExample.ListenForEvent(MyEventFunction);
            EventExample.ListenForEvent(MyEventFunction);
            EventExample.ListenForEvent(MyEventFunction);
            EventExample.ListenForEvent(MyOtherEventFunction);
            EventExample.LaunchEvent();
        }

        static void MyEventFunction(object bob, EventArgs derp)
        {
            Console.WriteLine("Gas powered Stick!");

        }
        static void MyOtherEventFunction(object bob, EventArgs derp)
        {
            Console.WriteLine("OTHER EVENT");
        }
    }

    public delegate void EventDelegate(object sender, EventArgs e);
    public class EventExample
    {
        private static event EventDelegate myEvent;

        public static void LaunchEvent()
        {
            if (myEvent != null)
            {
                myEvent(new Object(), EventArgs.Empty);
            }
        }
        public static void ListenForEvent(EventDelegate d)
        {
            myEvent += d;
        }

        public static void StopListeningForEvent(EventDelegate d)
        {
            myEvent -= d;
        }
    }
    public interface IAddable<T>
    {
        T add(T other);
    }
    public class MyClass
    {
        public int num;

        public MyClass(int num)
        {
            this.num = num;
        }
        public T add<T>(T a, T b) where T : IAddable<T>
        {
            return a.add(b);
        }
    }
    public class LinkedList<T> where T : INode
    {

        public void output()
        {
            LinkedList<T>.LinkedListNode reader = head;
            do
            {
                //Console.WriteLine(reader.value);
                reader.value.Output();
                reader = reader.next;

            } while (reader != null);
        }
        public class LinkedListNode
        {
            public LinkedListNode next;
            public T value;

            public LinkedListNode(T value)
            {
                this.value = value;
            }

            internal void SetNext(LinkedListNode nextNode)
            {
                this.next = nextNode;
            }
        }

        public LinkedListNode head;

        public LinkedList(params T[] objects)
        {

            foreach (T o in objects)
            {
                Insert(o);
            }
        }


        public void Insert(T value)
        {
            if (head == null)
            {
                head = new LinkedListNode(value);
                return;
            }
            LinkedListNode val = head;
            while (val.next != null)
            {
                val = val.next;
            }
            val.SetNext(new LinkedListNode(value));
        }

        public void Remove(T value)
        {
            if (head.value.Equals(value))
            {
                //
                head = head.next;
                return;
            }
            LinkedListNode currentNode = head.next;
            LinkedListNode prevNode = head;
            while (currentNode != null)
            {
                if (currentNode.value.Equals(value))
                {
                    //
                    prevNode.next = currentNode.next;
                    currentNode = null;
                    break;
                }
                else
                {
                    //
                    prevNode = currentNode;
                    currentNode = currentNode.next;
                }
            }
        }
    }
}
