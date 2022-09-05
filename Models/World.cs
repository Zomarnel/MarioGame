
namespace Models
{
    public class World
    {
        public List<WorldEntity> WorldEntities { get; init; }
        public World(IEnumerable<WorldEntity> worldEntities)
        {
            WorldEntities = worldEntities.ToList();
        }
        public List<Block> ReturnBlocks()
        {
            List<WorldEntity> worldEntities = WorldEntities.Where(w => w is Block).ToList();

            List<Block> blocks = new List<Block>();

            foreach(WorldEntity worldEntity in worldEntities)
            {
                blocks.Add(new Block(worldEntity.FileName, worldEntity.MapID, worldEntity.XCoordinate, worldEntity.YCoordinate,
                                    worldEntity.HorizontalSpeed, worldEntity.VerticalSpeed, worldEntity.Width, worldEntity.Height));
            }

            return blocks;

        }
    }
}
