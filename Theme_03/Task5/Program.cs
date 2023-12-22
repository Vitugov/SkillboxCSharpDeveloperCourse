using GeneralLibrary;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the game 'Guess the number'. You input max number,");
            Console.WriteLine("and I'll guess the number from 0 to your max number.");
            Console.WriteLine("Try to guess! If you don't guess I'll give you a hint.");
            var maxNumber = InputHandler.GetNumber("Input max number:", out var stopApp);
            var guessedNumber = (new Random()).Next(maxNumber);
            while (true)
            {
                var userNumber = InputHandler.GetNumber("Try to guess the number:", out stopApp);
                if (stopApp)
                    CloseApp(guessedNumber);
                if (userNumber == guessedNumber)
                {
                    Console.WriteLine("You guess!!! the number was " + guessedNumber);
                    break;
                }
                var result = userNumber > guessedNumber ? "less" : "larger";
                Console.WriteLine("Guessed number is " + result);
            }
        }

        public static void CloseApp(int arg)
        {
            Console.WriteLine("Guessed number was " + arg + ". You failed!");
            Environment.Exit(0);
        }
    }
}