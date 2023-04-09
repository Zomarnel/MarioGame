
namespace Models
{
    public class Enemy : WorldEntity
    {
        public int EntityID { get; set; }
        public int SpriteID { get; set; }

        public double InitialY { get; set; }

        public bool HasBeenKilled { get; set; } = false;

        // For Turtle Enemy
        public bool IsShelled = false;

        public bool PlayerInteractable = true;

        public Enemy(string fileName, int mapID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height, int entityID, int spriteID)
                    : base(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            EntityID = entityID;

            SpriteID = spriteID;
        }
        public void PlayerInteractionCooldown()
        {
            void localThread()
            {
                Thread.Sleep(200);

                PlayerInteractable = true;
            }

            PlayerInteractable = false;

            Thread updateThread = new Thread(localThread);
            updateThread.Start();
        }

        // Cooldown before disappering
        public void EnemyDeathCooldown()
        {
            void localThread()
            {
                Thread.Sleep(100);

                XCoordinate = -999;
                YCoordinate = -999;
            }

            Thread updateThread = new Thread(localThread);
            updateThread.Start();
        }
    }
}
