
namespace GameOfLife
{
    internal class Program
    {

        // Ограничения игры
        private const int Heigth = 30;
        private const int Width = 50;
        private const uint MaxRuns = 1000;
        private static LifeSimulationStartParameters StartParameters
            = new LifeSimulationStartParameters(Heigth, Width, 1, 10, 20, 2);

        private static void Main(string[] args)
        {
            int runs = 0;
            LifeSimulation sim = new LifeSimulation(StartParameters);

            while (runs++ < MaxRuns)
            {
                sim.DrawAndGrow();

                // Дадим пользователю шанс увидеть, что происходит, немного ждем
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
