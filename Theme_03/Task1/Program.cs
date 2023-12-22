using GeneralLibrary;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = InputHandler.GetNumber("Please, input number:", out var stopApp);
            if (result % 2 == 0)
                Console.WriteLine("Number is even");
            else
                Console.WriteLine("Number is odd");
            Console.ReadKey();
        }
    }
}