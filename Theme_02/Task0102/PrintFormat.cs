using System.Reflection;

namespace Task0102
{
    public enum PrintFormats
    {
        Detailed,
        Summary,
        Full,
        Random
    }
    public class PrintFormat
    {
        public static Dictionary<Type, Dictionary<PrintFormats, PrintFormat>> All;
        public Type Type { get; }
        public PrintFormats Name { get; }
        public List<string> PropertiesAsStrings { get; }
        public List<PropertyInfo> Properties { get; }
        public List<string> Header { get; }
        public string FormatString { get; }

        static PrintFormat()
        {
            All = new Dictionary<Type, Dictionary<PrintFormats, PrintFormat>>();
            CreateBasicFormats();
        }
        public PrintFormat(PrintFormats name, Type type, List<string> propertiesAsStrings)
        {
            Type = type;
            Name = name;
            PropertiesAsStrings = propertiesAsStrings;
            Properties = GetProperties(Type, PropertiesAsStrings);
            CheckAttributes(Type, Properties);
            Header = GenerateHeader(Properties);
            FormatString = GenerateFormatString(Properties);
            AddToDictionary();
        }
        public static string GenerateFormatString(List<PropertyInfo> propertiesToPrint)
        {
            var counter = 0;
            var formatString = propertiesToPrint
                .Select(property => "| {" + counter++ + "," + property.GetCustomAttribute<LabelAttribute>().PrintedFormat + "} ");
            return string.Concat(formatString) + "|";
        }

        public static List<string> GenerateHeader(List<PropertyInfo> propertiesToPrint)
        {
            var result = propertiesToPrint
                .Select(property => property.GetCustomAttribute<LabelAttribute>().NameToPrint)
                .ToList();
            return result;
        }

        public static List<PropertyInfo> GetProperties(Type type, List<string> propertiesToPrint)
        {
            var result = type.GetProperties()
                .Where(x => propertiesToPrint.Contains(x.Name))
                .OrderBy(x => propertiesToPrint.IndexOf(x.Name))
                .ToList();
            return result;
        }

        public static void CheckAttributes(Type type, List<PropertyInfo> propertiesToPrint)
        {
            var propertiesWithoutAttributes = propertiesToPrint
                .Select(property => property.GetCustomAttribute<LabelAttribute>())
                .Where(value => value == null).ToList();
            if (propertiesWithoutAttributes.Count > 0)
                throw new Exception("Add LabelAttribute to Properties to be able to Print it");
        }

        public void AddToDictionary()
        {
            if (!All.ContainsKey(Type))
                All.Add(Type, new Dictionary<PrintFormats, PrintFormat>());
            All[Type][Name] = this;
        }

        public static void CreateBasicFormats()
        {
            new PrintFormat(PrintFormats.Detailed, typeof(StudentScores), new List<string>
                { "FullName", "Email", "Age", "ProgrammingScores", "MathScores", "PhysicsScores" });
            new PrintFormat(PrintFormats.Summary, typeof(StudentScores), new List<string>
                { "FullName", "Email", "Age", "SumScores", "AverageScores" });
            new PrintFormat(PrintFormats.Full, typeof(StudentScores), new List<string>
                { "FullName", "Email", "Age", "ProgrammingScores", "MathScores", "PhysicsScores", "SumScores", "AverageScores" });
            GenerateNewRandomFormat();
        }

        public static void GenerateNewRandomFormat()
        {
            var properties = new List<string>
                { "FullName", "Email", "Age", "ProgrammingScores", "MathScores", "PhysicsScores", "SumScores", "AverageScores" };
            var rnd = new Random();
            var rndPropeties = properties.OrderBy(item => rnd.Next()).Take(rnd.Next(1, 8)).ToList();
            new PrintFormat(PrintFormats.Random, typeof(StudentScores), rndPropeties);
        }

    }
}
