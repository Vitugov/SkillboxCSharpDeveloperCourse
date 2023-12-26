namespace Task2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Input sentence, please:");
            string inputPhrase = Console.ReadLine();
            string reversedPhrase = ReverseWords(inputPhrase);
            Console.WriteLine("Reversed sentence:");
            Console.WriteLine(reversedPhrase);
        }

        static string ReverseWords(string inputPhrase)
        {
            var words = SplitText(inputPhrase).ToArray();
            var result = string.Join(" ", words.Reverse()).Trim();
            return result;
        }

        static string[] SplitText(string text) => text.Split(' ');
    }
}
