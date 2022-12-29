
namespace Models
{
    public class GlowingTask : ITask
    {
        public WorldEntity WorldEntity { get; set; }
        public bool IsFulfilled { get; set; } = false;

        public GlowingTask(WorldEntity worldEntity)
        {
            WorldEntity = worldEntity;
        }
        public void Execute()
        {
            switch (WorldEntity.FileName)
            {
                case "LuckyBlock":
                    WorldEntity.FileName = "LuckyBlockGlow";
                    break;
                case "LuckyBlockGlow":
                    WorldEntity.FileName = "LuckyBlockGlowGlow";
                    break;
                case "LuckyBlockGlowGlow":
                    WorldEntity.FileName = "LuckyBlock";
                    IsFulfilled = true;
                    break;
            }

            WorldEntity.NeedsToBeUpdated = true;

        }
    }
}
