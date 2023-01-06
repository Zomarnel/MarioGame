namespace Models
{
    public class WorldEntity : BaseEntity
    {
        public string FileName { get; set; }
        public int WorldID { get; init; }
        public bool NeedsToBeUpdated { get; set; } = false;
        public bool HasBeenDrawn { get; set; } = false;
        public bool PlayerHasInteracted { get; set; } = false;
        public WorldEntity(string fileName, int worldID, double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height) : base(xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            FileName = fileName;
            WorldID = worldID;
        }

    }
}
