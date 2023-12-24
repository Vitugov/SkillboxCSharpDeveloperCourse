using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace GameOfLife
{
    public static class Extensions
    {
        public static List<Creature> Randomize(this List<Creature> list)
        {
            var rnd = new Random();
            return list.OrderBy(x => rnd.Next()).ToList();
        }
    }

    public class LifeSimulationStartParameters
    {
        private Random random;
        public int Height { get; }
        public int Width { get; }
        public Dictionary<Type, double> WeightsOfCreatures { get; set; }
        public Type Dice
        {
            get
            {
                var value = random.NextDouble();
                if (value < WeightsOfCreatures[typeof(Wolf)])
                    return typeof(Wolf);
                if (value < WeightsOfCreatures[typeof(Sheep)] + WeightsOfCreatures[typeof(Wolf)])
                    return typeof(Sheep);
                if (value < 1 - WeightsOfCreatures[typeof(Ground)])
                    return typeof(Grass);
                return typeof(Ground);
            }
        }
        
        public LifeSimulationStartParameters(int height, int width, double wolfWeight, double sheepWeight, double grassWeight, double GroundWeight)
        {
            random = new Random();
            Height = height;
            Width = width;
            var WeightSum = wolfWeight + sheepWeight + grassWeight + GroundWeight;
            WeightsOfCreatures = new Dictionary<Type, double>();
            WeightsOfCreatures[typeof(Wolf)] = wolfWeight / WeightSum;
            WeightsOfCreatures[typeof(Sheep)] = sheepWeight / WeightSum;
            WeightsOfCreatures[typeof(Grass)] = grassWeight / WeightSum;
            WeightsOfCreatures[typeof(Ground)] = GroundWeight / WeightSum;
        }

    }
    public class LifeSimulation
    {
        public LifeSimulationStartParameters StartParameters { get; set; }
        private int Height => StartParameters.Height;
        private int Width => StartParameters.Width;
        private Creature[,] Cells { get; set; }

        public bool[,] IsProcessed { get; set; }

        public LifeSimulation(LifeSimulationStartParameters startParameters)
        {
            StartParameters = startParameters;
            Cells = new Creature[Height, Width];
            IsProcessed = new bool[Height, Width];
            GenerateField();
        }

        public Creature this[Creature creature]
        {
            get => Cells[creature.Place.X, creature.Place.Y];
            set
            {
                Cells[creature.Place.X, creature.Place.Y] = value;
                IsProcessed[creature.Place.X, creature.Place.Y] = true;
            }
        }

        public ConsoleColor SetlColour(int i, int j) => Console.ForegroundColor = Cells[i, j].Profile.Color;
        
        /// <summary>
        /// Перейти к следующему поколению и вывести результат на консоль.
        /// </summary>
        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        /// <summary>
        /// Двигаем состояние на одно вперед, по установленным правилам
        /// </summary>

        private void Grow()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    IsProcessed[i, j] = false;
                }
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (!IsProcessed[i, j])
                        Cells[i,j].Grow();
                }
            }
        }

        /// <summary>
        /// Смотрим сколько живых соседий вокруг клетки.
        /// </summary>
        /// <param name="x">X-координата клетки.</param>
        /// <param name="y">Y-координата клетки.</param>
        /// <returns>Число живых клеток.</returns>

        public List<Creature> GetNeighborsOfType(Type type, Vector vector)
        {
            var result = GetNeighbors(vector)
                .Where(creature => creature.GetType() == type)
                .ToList();
            return result;
        }

        public List<Creature> GetNeighbors(Vector vector)
        {
            var result = GetNeighborCells(vector)
                .Select(vector => Cells[vector.X, vector.Y])
                .ToList();
            return result;
        }

        public List<Vector> GetNeighborCells(Vector vector)
        {
            var result = vector.GetNearbyVectors()
                .Where(v => v.X >= 0 && v.Y >= 0 && v.X < Height && v.Y < Width)
                .ToList();
            return result;
        }

        /// <summary>
        /// Нарисовать Игру в консоле
        /// </summary>

        private void DrawGame()
        {
            var defaultColor = Console.ForegroundColor;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.ForegroundColor = Cells[i, j].Profile.Color;
                    Console.Write(Cells[i, j].Profile.Symbol);
                }
                Console.WriteLine("\r");
            }
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        /// <summary>
        /// Инициализируем случайными значениями
        /// </summary>

        private void GenerateField()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Activator.CreateInstance(StartParameters.Dice, this, new Vector(i,j));

                }
            }
        }
    }

    internal class Program
    {

        // Ограничения игры
        private const int Heigth = 10;
        private const int Width = 30;
        private const uint MaxRuns = 100;
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
