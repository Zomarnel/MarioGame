
namespace Models
{
    public class Enemy : WorldEntity
    {
        public int EntityID { get; set; }
        public int SpriteID { get; set; }

        public double InitialY { get; set; }

        public Enemy(string fileName, int mapID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height, int entityID, int spriteID)
                    : base(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            EntityID = entityID;

            SpriteID = spriteID;
        }
    }
}
