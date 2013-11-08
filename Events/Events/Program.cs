using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    public delegate void MyDelegate(object sender, EventArgs e);

    public class EventManager
    {
        private static EventManager manager;
        private Dictionary<string, MyDelegate> events = new Dictionary<string, MyDelegate>();

        public static EventManager Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new EventManager();
                }
                return manager;
            }
        }

        private EventManager() { }

        public void Happen(string eventName)
        {
            if (!events.Keys.Contains(eventName)) return;
            if (events[eventName] != null)
            {
                events[eventName](this, EventArgs.Empty);
            }
        }

        public void Subscribe(string eventName, MyDelegate subscriber)
        {
            if (!events.Keys.Contains(eventName))
            {
                events.Add(eventName, null);
            }
            events[eventName] += subscriber;
        }

        public void Forget(string eventName, MyDelegate subscriber)
        {
            if (events.Keys.Contains(eventName))
            {
                events[eventName] -= subscriber;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EventManager.Manager.Subscribe("cat",Cats);
            EventManager.Manager.Subscribe("dog", Dogs);
            EventManager.Manager.Subscribe("other", Other);

            string response = "";

            while (true)
            {
                Console.WriteLine("What kind of animal is good as a pet? [Enter 'End' to quit]");
                response = Console.ReadLine().ToLower();
                if (response == "end") break;
                if (response != "cat" && response != "dog")
                {
                    response = "other";
                }
                EventManager.Manager.Happen(response);
            }
            
        }

        static void Cats(object sender, EventArgs e)
        {
            Console.WriteLine("Cats are independent.");
        }

        static void Dogs(object sender, EventArgs e)
        {
            Console.WriteLine("Dogs are so needy.");
        }

        static void Other(object sender, EventArgs e)
        {
            Console.WriteLine("That's interesting.");
        }

    }
}
