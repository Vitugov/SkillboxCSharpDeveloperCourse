using GeneralLibrary;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var CardsNumber = InputHandler.GetNumber("Please input how many cards do you have?", out var stopApp);
            var sum = 0;
            for (int i = 0; i < CardsNumber; i++)
                sum += GetCardValue();
            Console.WriteLine("Sum of your cards values is: " + sum);
            Console.ReadKey();


        }

        public static int GetCardValue()
        {
            var cards = new HashSet<string>() { "J", "Q", "K", "T", "j", "q", "k", "t" };
            int result = 10;
            while (true)
            {
                Console.WriteLine("Please input next card");
                var input = Console.ReadLine();
                if (input != null && cards.Contains(input))
                    break;
                if (int.TryParse(input, out result) && (result <= 10 && result >= 2))
                    break;
                Console.WriteLine("Please try one more time, input should be a number");
                Console.WriteLine("from 2 to 10 or one of symbols: J, Q, K, T");
                Console.WriteLine("");
            }
            return result;
        }
    }
}