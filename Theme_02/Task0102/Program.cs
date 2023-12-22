
namespace Task0102
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            var studentScoreFormats = PrintFormat.All[typeof(StudentScores)];
            StudentScores.PrintAllStudents(studentScoreFormats[PrintFormats.Detailed]);
            Console.ReadKey();
            StudentScores.PrintAllStudents(studentScoreFormats[PrintFormats.Summary]);
            Console.ReadKey();
            //StudentScores.PrintAllStudents(studentScoreFormats[PrintFormats.Full]);
            //Console.ReadKey();
        }

        public static void Initialize()
        {
            new StudentScores("Adam Adams", "adamadams@gmail.com", 17, 102, 97, 86);
            new StudentScores("John Johns", "johnjohns@gmail.com", 18, 89, 84, 94);
        }
    }
}