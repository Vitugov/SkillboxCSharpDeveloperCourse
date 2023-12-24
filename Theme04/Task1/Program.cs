using Library;
namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rows = InputHandler.GetNumber("Input amount of matrix rows", out var stopApp);
            var columns = InputHandler.GetNumber("Input amount of matrix columns", out stopApp);
            Console.WriteLine();
            var matrix = Matrix.GetRandomMatrix(rows, columns);
            matrix.Print("M");
            Console.ReadKey();
        }
    }
}