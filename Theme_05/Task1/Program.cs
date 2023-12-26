
namespace Program
{    
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input sentence, please:");
            string inputText = Console.ReadLine();
            string[] words = SplitText(inputText);
            Console.WriteLine("Every word in this sentence:");
            PrintWords(words);
        }

        public static string[] SplitText(string text)
        {
            string[] words = text.Split(' ');
            return words;
        }

        public static void PrintWords(string[] words)
        {
            foreach (string word in words)
                Console.WriteLine(word);
        }
    }
}