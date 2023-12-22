
namespace Task0102
{
    public class LabelAttribute : Attribute
    {
        public string NameToPrint { get; }
        public string PrintedFormat { get; }
        public LabelAttribute(string nameToPrint, string printedFormat)
        {
            NameToPrint = nameToPrint;
            PrintedFormat = printedFormat;
        }
    }
}
