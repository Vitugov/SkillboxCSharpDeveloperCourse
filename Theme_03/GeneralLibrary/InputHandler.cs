
namespace GeneralLibrary
{
    public class InputHandler
    {
        public static int GetNumber(string message, out bool stopApp)
        {
            stopApp = false;
            var result = 0;
            while (true)
            {
                if (message != "")
                    Console.WriteLine(message);
                var input = Console.ReadLine();
                if (input == "")
                {
                    stopApp = true;
                    break;
                }
                if (int.TryParse(input, out result))
                    break;
                Console.WriteLine("Please try one more time, number should");
                Console.WriteLine("be from -2,147,483,648 to 2,147,483,647");
                Console.WriteLine("");
            }
            return result;
        }
    }
}