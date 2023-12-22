using GeneralLibrary;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var IsPrimeNumber = true;
            var number = InputHandler.GetNumber("Please, input a number:", out var stopApp);
            for (int i = 2;  i < number / 2; i++)
                if (number % i == 0)
                    IsPrimeNumber = false;
            var message = IsPrimeNumber == false ? "Number isn't prime" : "Number is prime";
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}