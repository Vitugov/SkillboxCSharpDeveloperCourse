
namespace GameOfLife
{
    public class LifeSimulationStartParameters
    {
        private Random random;
        public int Height { get; }
        public int Width { get; }
        public Dictionary<Type, double> WeightsOfCreatures { get; set; }

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

        public Type GetDice()
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
}
