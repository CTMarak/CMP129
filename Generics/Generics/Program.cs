using System;
using System.Linq;
using System.Text;

namespace Generics
{
    public class LinkedList<T>
    {
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
                this.Insert(o);
            }
        }

        public void Insert(T value)
        {
            if (head == null)
            {
                head = new LinkedListNode(value);
            }
            else
            {
                LinkedListNode val = head;
                while (val.next != null)
                {
                    val = val.next;
                }
                val.SetNext(new LinkedListNode(value));
            }
        }

        public bool Remove(T value)
        {
            if (head.value.Equals(value))
            {
                head = head.next;
                return true;
            }
            else
            {
                LinkedListNode val = head;
                while (val.next != null)
                {
                    if (val.next.value.Equals(value))
                    {
                        val.SetNext(val.next.next);
                        return true;
                    }
                    val = val.next;
                }
            }
            return false;
        }

        public void RemoveAll(T value)
        {
            bool success;
            do
            {
                success = this.Remove(value);
            } while (success);
        }

        public void Display()
        {
            LinkedListNode val = head;
            do
            {
                Console.Write(val.value.ToString() + " ");
                val = val.next;
            } while (val != null);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> intList = new LinkedList<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
            intList.Display();
            intList.Insert(22);
            intList.Insert(5);
            intList.Display();
            intList.Remove(5);
            intList.Remove(7);
            intList.Display();
            intList.Insert(1);
            intList.Insert(1);
            intList.Insert(1);
            intList.Insert(1);
            intList.Insert(1);
            intList.Insert(1);
            intList.Display();
            intList.RemoveAll(1);
            intList.Display();
        }
    }
}
