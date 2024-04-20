using System.Text.RegularExpressions;

namespace myOperatorOverloading
{
    class WaitingList
    {
        public int Number {  get; set; }
        public int Minutes { get; set; }
        public string Name { get; set; }

        public WaitingList()
        {
            Number = 0;
            Minutes = 0;
            Name = string.Empty;
        }
        public WaitingList(int num, int minutes, string name)
        {
            Number = num;
            Minutes = minutes;
            Name = name;
        }

        public static int operator +(int total, WaitingList wait)
        {
            total += wait.Number;
            return total;
        }
        public static int operator -(int total, WaitingList wait)
        {
            total -= wait.Number;
            return total;
        }

        public static bool operator ==(WaitingList wait1, WaitingList wait2)
        {
            bool sameMins = false;
            if (wait1.Minutes == wait2.Minutes)
                sameMins = true;
            return sameMins;
        }
        public static bool operator !=(WaitingList wait1, WaitingList wait2)
        {
            bool notSameMins = false;
            if (wait1.Minutes != wait2.Minutes)
                notSameMins = true;
            return notSameMins;
        }
        public static bool operator >(WaitingList wait1, WaitingList wait2)
        {
            bool longer = false;
            if (wait1.Minutes > wait2.Minutes)
                longer = true;
            return longer;
        }
        public static bool operator <(WaitingList wait1, WaitingList wait2)
        {
            bool shorter = false;
            if (wait1.Minutes < wait2.Minutes)
                shorter = true;
            return shorter;
        }

        public static WaitingList operator ++(WaitingList wait)
        {
            wait.Number++;
            return wait;
        }
        public static WaitingList operator --(WaitingList wait)
        {
            wait.Number--;
            return wait;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            IList<WaitingList> waits = new List<WaitingList>()
            {
                new (r.Next(1, 5), r.Next(5, 15), "Group1"),
                new (r.Next(1, 5), r.Next(5, 15), "Group2"),
                new (r.Next(1, 5), r.Next(5, 15), "Group3"),
                new (r.Next(1, 5), r.Next(5, 15), "Group4"),
                new (r.Next(1, 5), r.Next(5, 15), "Group5")
            };

            Console.WriteLine("Welcome! How many in your party?");
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Input should be a whole number! How many in your party?");
            }
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            waits.Add(new (num, r.Next(5, 15), name));

            int total = 0;
            Console.WriteLine($"\n*----- Waiting List -----*\n Number of parties: {waits.Count}");
            for(int i = 0; i < waits.Count; i++)
            {
                Console.WriteLine($" {waits[i].Name}\t - {waits[i].Number} people");
                total += waits[i];
            }
            Console.WriteLine($"\nThere are {total} people waiting.");
            for (int i = 0; i < 10; i++)
                Console.WriteLine(".");
            Console.WriteLine("\n\nOpen!!\n\n");

            for(int i = 0; i < waits.Count; i++)
            {
                Console.WriteLine($"\n{waits[i].Name}, your table's ready! Thank you for waiting:)");
                if (i != 0)
                {
                    if (waits[i] == waits[i - 1])
                        Console.WriteLine($"{waits[i].Name} and {waits[i - 1].Name} have the same wait time.");
                    else if (waits[i] > waits[i - 1])
                        Console.WriteLine($"{waits[i].Name} waited longer than {waits[i - 1].Name}.");
                    else
                        Console.WriteLine($"{waits[i].Name} waited shorter than {waits[i - 1].Name}.");
                }
                total -= waits[i];
                Console.WriteLine($"There are {total} people waiting.");
            }
            for (int i = 0; i < 5; i++)
                Console.WriteLine(".");

            int add = r.Next(1, 6);
            waits[add]++;
            Console.WriteLine($"\nA new person has been added to Group{add}. Now Group{add} has {waits[add].Number} people.");

            int leave = r.Next(1, 6);
            waits[leave]--;
            Console.WriteLine($"A person has left Group{leave}. Now Group {leave} has {waits[leave].Number} people.");
        }
    }
}
