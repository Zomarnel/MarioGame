namespace Models
{
    public class World
    {
        public List<Block> Blocks { get; set; }
        public List<Enemy> Enemies { get; set; }
        public double WorldXCoordinate { get; set; } = 0;
    }
}
