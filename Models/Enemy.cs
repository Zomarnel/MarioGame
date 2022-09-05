
namespace Models
{
    public class Enemy : WorldEntity
    {
        public Enemy(string fileName, int mapID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height)
                    : base(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {

        }
    }
}
