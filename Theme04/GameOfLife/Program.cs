
namespace GameOfLife
{
    internal class Program
    {

        // Ограничения игры
        private const int Heigth = 27;
        private const int Width = 60;
        private const uint MaxRuns = 1000;
        private static StartParameters StartParameters
            = new StartParameters(Heigth, Width, 2, 10, 20, 2);

        private static void Main(string[] args)
        {
            int runs = 0;
            Game sim = new Game(StartParameters);

            while (runs++ < MaxRuns)
            {
                sim.DrawAndGrow();
                Thread.Sleep(100);
            }
        }
    }
}
