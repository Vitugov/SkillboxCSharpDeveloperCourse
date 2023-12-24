using Library;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rows = InputHandler.GetNumber("Input amount of matrix rows", out var stopApp);
            var columns = InputHandler.GetNumber("Input amount of matrix columns", out stopApp);
            Console.WriteLine();
            var m1 = Matrix.GetRandomMatrix(rows, columns);
            m1.Print("M1", false);
            var m2 = Matrix.GetRandomMatrix(rows, columns);
            m2.Print("M2", false);
            var mSum = m1 + m2;
            mSum.Print("Sum of M1 & M2", false);
            Console.ReadKey();
        }
    }
}