namespace Models
{
    public class WorldEntity : Entity
    {
        public string FileName { get; set; }
        public int MapID { get; init; }
        public EventHandler OnPlayerCollision { get; init; }
        public EventHandler OnUpdate { get; init; }
        public WorldEntity(string fileName, int mapID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height) : base(xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            FileName = fileName;
            MapID = mapID;
        }
    }
}
