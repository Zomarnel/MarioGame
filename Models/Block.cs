
namespace Models
{
    public class Block : WorldEntity
    {

        public Block(string fileName, int mapID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height)
                    : base(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
        }
    }
}
