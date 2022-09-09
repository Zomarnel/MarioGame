namespace Models
{
    public class WorldEntity : Entity
    {
        public string FileName { get; set; }
        public int WorldID { get; init; }
        public bool NeedsToBeUpdated { get; set; } = false;
        public bool IsUpdating { get; set; } = false;
        public bool HasBeenDrawn { get; set; } = false;
        public EventHandler OnPlayerCollision { get; set; }
        public EventHandler OnUpdate { get; set; }
        public WorldEntity(string fileName, int worldID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height) : base(xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            FileName = fileName;
            WorldID = worldID;
        }
    }
}
