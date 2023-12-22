
namespace Task0102
{
    public class StudentScores
    {
        public static List<StudentScores> All { get; }

        [Label("Student name", "-20")]
        public string FullName { get; }
        
        [Label("E-mail", "-25")]
        public string Email { get; }
        
        [Label("Age", "6")]
        public byte Age { get; }
        
        [Label("Programming", "17: #.##")]
        public double ProgrammingScores { get; }
        
        [Label("Mathematics", "17: #.##")]
        public double MathScores { get; }
        
        [Label("Physics", "17: #.##")]
        public double PhysicsScores { get; }
        
        [Label("Scores sum", "17: #.##")]
        public double SumScores => ProgrammingScores + MathScores + PhysicsScores;
        
        [Label("Average score", "17: #.##")]
        public double AverageScores => SumScores / 3;

        static StudentScores()
        {
            All = new List<StudentScores>();
        }

        public StudentScores(string fullName, string email, byte age, double programmingScores, double mathScores, double physicsScores)
        {
            FullName = fullName;
            Email = email;
            Age = age;
            ProgrammingScores = programmingScores;
            MathScores = mathScores;
            PhysicsScores = physicsScores;
            All.Add(this);
        }

        public static void PrintAllStudents(PrintFormat printFormat)
        {
            PrintHeader(printFormat);
            foreach (var student in All)
                Console.WriteLine(printFormat.FormatString, printFormat.Properties
                    .Select(property => property.GetValue(student)).ToArray<object?>());
            Console.WriteLine();
        }

        public static void PrintHeader(PrintFormat printFormat)
        {
            Console.WriteLine(printFormat.Name);
            Console.WriteLine(printFormat.FormatString, printFormat.Header.ToArray<object>());
        }
    }
}
