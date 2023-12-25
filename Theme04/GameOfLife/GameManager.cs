
namespace GameOfLife
{
    public class GameManager
    {
        private Dictionary<Creature, Vector> creaturesPlaces { get; }
        private int height { get;}
        private int width {  get;}
        private Creature[,] cells { get; }
        private bool[,] isProcessed { get; }

        public GameManager(StartParameters startParameters)
        {
            creaturesPlaces = new Dictionary<Creature, Vector>();
            height = startParameters.Height;
            width = startParameters.Width;
            cells = new Creature[height, width];
            isProcessed = new bool[height, width];
        }

        public Vector GetVector(Creature creature)
        {
            if (!Contains(creature))
                throw new ArgumentException();
            return creaturesPlaces[creature];
        }

        public bool Contains(Creature creature)
        {
            return creaturesPlaces.ContainsKey(creature);
        }

        public Creature GetCreature(Vector vector)
        {
            if (cells[vector.X, vector.Y] == null)
                throw new ArgumentException();
            return cells[vector.X, vector.Y];
        }

        public void Add(Creature creature, Vector vector)
        {
            if (cells[vector.X, vector.Y] != null)
                throw new ArgumentException("Field isn't clear to add new Creature");
            if (Contains(creature))
                throw new ArgumentException("Creature already is in Dictionary. Don't ypu forget to delete it?");
            
            cells[vector.X, vector.Y] = creature;
            creaturesPlaces.Add(creature, vector);
            
            isProcessed[vector.X, vector.Y] = true;
        }

        public void Remove(Creature creature)
        {
            
            var vector = GetVector(creature);
            cells[vector.X, vector.Y] = null;
            creaturesPlaces.Remove(creature);
            
            isProcessed[vector.X, vector.Y] = true;
        }

        public void Move(Creature creature, Vector vector)
        {
            if (cells[vector.X, vector.Y] != null)
                throw new ArgumentException("Field isn't clear to move Creature");
            var oldVector = GetVector(creature);
            
            cells[oldVector.X, oldVector.Y] = null;
            cells[vector.X, vector.Y] = creature;
            creaturesPlaces[creature] = vector;

            isProcessed[oldVector.X, oldVector.Y] = true;
            isProcessed[vector.X, vector.Y] = true;
        }

        public bool IsUsed(int x, int y) => isProcessed[x, y];

        public void ReSet()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    isProcessed[i, j] = false;   
        }

        public List<Creature> GetNeighborsOfType(Type type,Creature creature)
        {
            var v = GetVector(creature);
            var result = v.GetNearbyVectors()
                .Where(v => v.X >= 0 && v.Y >= 0 && v.X < height && v.Y < width)
                .Select(vector => cells[vector.X, vector.Y])
                .Where(creature => creature.GetType() == type)
                .ToList();
            return result;
        }

        public void Cycle(Game lifeSimulation, params Action<Game, int, int>[] act )
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                    act[0](lifeSimulation, i, j);
                if (act.Length > 1)
                    act[1](lifeSimulation, i, 0);
            }
        }

        public void Check()
        {
            if (creaturesPlaces.Count != height * width)
                throw new Exception();
        }
    }
}
