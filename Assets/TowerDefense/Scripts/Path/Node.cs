namespace TowerDefense.Scripts.Path
{
    public class Node
    {
        public Point Position { get; }
        public float GScore { get; }

        public Node(Point position, float gScore)
        {
            Position = position;
            GScore = gScore;
        }
    }
}