using GeneralLibrary;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var minimum = int.MaxValue;
            var amount = InputHandler.GetNumber("Please, input how many numbers in sequence:", out var stopApp);
            Console.WriteLine("Please, input numbers one by one. After inputing each number, press Enter");
            for (int i = 0; i < amount; i++)
            {
                var number = InputHandler.GetNumber("", out stopApp);
                minimum = Math.Min(minimum, number);
            }
            Console.WriteLine("Minimum number is: " + minimum);
            Console.ReadKey();
        }
    }
}