
namespace Models
{
    public class Block
    {
        public int Width { get; init; }
        public int Height { get; init; }

        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

        /*public enum BlockType
        {
            BrickBlock,
            LuckyBlock,
            BlankBlock,
            StairsBlock,
            Tunnel
        }*/

        public string Name { get; init; }    
        public Block(string name, int width, int height, double xCoordinate, double yCoordinate)
        {
            Name = name;

            Width = width;
            Height = height;

            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

        public bool IsPointInsideBlock(double xCoordinate, double yCoordinate)
        {
            if (xCoordinate > XCoordinate && xCoordinate < XCoordinate + Width && yCoordinate > YCoordinate && yCoordinate < YCoordinate + Height)
            {
                return true;
            }

            return false;
        }
    }
}
