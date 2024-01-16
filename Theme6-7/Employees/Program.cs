namespace Employees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var worker = Worker.Add();
            Worker.Print(new List<IStoredData>() { worker });
            Worker.Print(IStoredData.Data[typeof(Worker)].Values.ToList());
        }
    }
}