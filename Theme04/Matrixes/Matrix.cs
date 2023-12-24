using System.Data;

namespace Library
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        public int[,] M { get; set; }
        public int Sum => GetElementsSum(this);

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            M = new int[Rows, Columns];
        }

        public static Matrix GetRandomMatrix(int rows, int columns)
        {
            var matrix = new Matrix(rows, columns);
            var rnd = new Random();
            for(int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix.M[i,j] = rnd.Next(100);
            return matrix;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return Cycle(m1, m2, (x, y) => x + y);
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return Cycle(m1, m2, (x, y) => x - y);
        }

        public int GetElementsSum(Matrix m)
        {
            var sum = 0;
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Columns; j++)
                    sum += m.M[i, j];
            return sum;
        }

        public static Matrix Cycle(Matrix m1, Matrix m2, Func<int, int, int> func)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                throw new ArgumentException("To Add matrixes they should be the same size");
            var matrix = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Columns; j++)
                    matrix.M[i, j] = func(m1.M[i, j], m2.M[i, j]);
            return matrix;
        }

        public void Print(string name, bool withSum = true)
        {
            if (Rows > 100 || Columns > 100)
            {
                Console.WriteLine("Matrix {0}x{1} is too big to print", Rows, Columns);
                Console.WriteLine();
                return;
            }
            Console.WriteLine("Matrix {0}:", name);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    Console.Write(" {0, 3} ", M[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
            if (withSum)
                Console.WriteLine("Matrix elements sum: " + Sum);
            Console.WriteLine();
        }
    }
}