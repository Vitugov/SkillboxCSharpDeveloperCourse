using System;

public class InputHandler
{
    public static int GetNumber(string message)
    {
        var result = 0;
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if (int.TryParse(input, out result))
                break;
            Console.WriteLine("Please try one more time, number should");
            Console.WriteLine("be from -2,147,483,648 to 2,147,483,647");
            Console.WriteLine("");
        }
        return result;
    }
}
