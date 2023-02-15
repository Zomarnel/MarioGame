using Core;
using Models;

namespace Services
{
    public static class WorldFactory
    {
        private static List<WorldEntity> _entities = new List<WorldEntity>();

        private static bool _isUpdatingLuckyBlocks = false;

        private static MovementTask? _movementTask;
        static WorldFactory()
        {
            #region WORLD-1
            
            //Floors
            AddNewBlock("", 0, 0, 0, 2208, 64);
            AddNewBlock("", 0, 2272, 0, 480, 64);
            AddNewBlock("", 0, 2848, 0, 2048, 64);
            AddNewBlock("", 0, 4960, 0, 1792, 64);

            //Blocks
            AddNewBlock("LuckyBlock", 0, 512, 160, 32, 32);
            AddNewBlock("Brick", 0, 640, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 672, 160, 32, 32);
            AddNewBlock("Brick", 0, 704, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 736, 160, 32, 32);
            AddNewBlock("Brick", 0, 768, 160, 32, 32);

            AddNewBlock("LuckyBlock", 0, 704, 288, 32, 32);

            AddNewBlock("Brick", 0, 2464, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 2496, 160, 32, 32);
            AddNewBlock("Brick", 0, 2528, 160, 32, 32);

            AddNewBlock("Brick", 0, 2560, 288, 32, 32);
            AddNewBlock("Brick", 0, 2592, 288, 32, 32);
            AddNewBlock("Brick", 0, 2624, 288, 32, 32);
            AddNewBlock("Brick", 0, 2656, 288, 32, 32);
            AddNewBlock("Brick", 0, 2688, 288, 32, 32);
            AddNewBlock("Brick", 0, 2720, 288, 32, 32);
            AddNewBlock("Brick", 0, 2752, 288, 32, 32);
            AddNewBlock("Brick", 0, 2784, 288, 32, 32);

            AddNewBlock("Brick", 0, 2912, 288, 32, 32);
            AddNewBlock("Brick", 0, 2944, 288, 32, 32);
            AddNewBlock("Brick", 0, 2976, 288, 32, 32);
            AddNewBlock("LuckyBlock", 0, 3008, 288, 32, 32);

            AddNewBlock("Brick", 0, 3008, 160, 32, 32);

            AddNewBlock("Brick", 0, 3200, 160, 32, 32);
            AddNewBlock("Brick", 0, 3232, 160, 32, 32);

            AddNewBlock("LuckyBlock", 0, 3392, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 3488, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 3488, 288, 32, 32);
            AddNewBlock("LuckyBlock", 0, 3584, 160, 32, 32);

            AddNewBlock("Brick", 0, 3776, 160, 32, 32);

            AddNewBlock("Brick", 0, 3872, 288, 32, 32);
            AddNewBlock("Brick", 0, 3904, 288, 32, 32);
            AddNewBlock("Brick", 0, 3936, 288, 32, 32);

            AddNewBlock("Brick", 0, 4096, 288, 32, 32);
            AddNewBlock("LuckyBlock", 0, 4128, 288, 32, 32);
            AddNewBlock("LuckyBlock", 0, 4160, 288, 32, 32);
            AddNewBlock("Brick", 0, 4192, 288, 32, 32);

            AddNewBlock("Brick", 0, 4128, 160, 32, 32);
            AddNewBlock("Brick", 0, 4160, 160, 32, 32);

            AddNewBlock("Brick", 0, 5376, 160, 32, 32);
            AddNewBlock("Brick", 0, 5408, 160, 32, 32);
            AddNewBlock("LuckyBlock", 0, 5440, 160, 32, 32);
            AddNewBlock("Brick", 0, 5472, 160, 32, 32);

            //Tunnels
            AddNewBlock("", 0, 900, 64, 55, 65);
            AddNewBlock("", 0, 1220, 64, 55, 95);
            AddNewBlock("", 0, 1477, 64, 55, 125);
            AddNewBlock("", 0, 1828, 64, 55, 125);
            AddNewBlock("", 0, 5222, 64, 55, 65);
            AddNewBlock("", 0, 5732, 64, 55, 65);

            //Stairs
            AddNewBlock("", 0, 4288, 0, 128, 96);
            AddNewBlock("", 0, 4320, 64, 96, 64);
            AddNewBlock("", 0, 4352, 96, 64, 64);
            AddNewBlock("", 0, 4384, 128, 32, 64);
            AddNewBlock("", 0, 4480, 0, 128, 96);
            AddNewBlock("", 0, 4480, 64, 96, 64);
            AddNewBlock("", 0, 4480, 96, 64, 64);
            AddNewBlock("", 0, 4480, 128, 32, 64);
            AddNewBlock("", 0, 4736, 0, 160, 96);
            AddNewBlock("", 0, 4768, 64, 128, 64);
            AddNewBlock("", 0, 4800, 96, 96, 64);
            AddNewBlock("", 0, 4832, 128, 64, 64);
            AddNewBlock("", 0, 4960, 0, 128, 96);
            AddNewBlock("", 0, 4960, 64, 96, 64);
            AddNewBlock("", 0, 4960, 96, 64, 64);
            AddNewBlock("", 0, 4960, 128, 32, 64);
            AddNewBlock("", 0, 5792, 0, 288, 96);
            AddNewBlock("", 0, 5824, 64, 256, 64);
            AddNewBlock("", 0, 5856, 96, 224, 64);
            AddNewBlock("", 0, 5888, 128, 192, 64);
            AddNewBlock("", 0, 5920, 160, 160, 64);
            AddNewBlock("", 0, 5952, 192, 128, 64);
            AddNewBlock("", 0, 5984, 224, 96, 64);
            AddNewBlock("", 0, 6016, 256, 64, 64);

            AddNewBlock("", 0, 6336, 64, 32, 32);

            #endregion WORLD-1
        }
        public static World GetWorldByID(int worldID)
        {
            World world = new World();

            PopulateWorldObject(world, worldID);

            return world;
        }

        #region Update World
        public static List<Block> ReturnDisposableBlocks(World world)
        {
            //Blocks that will be removed on the canvas's children
            List<Block> blocksToReturn = new List<Block>();

            //Blocks that will be removed on the world's list of blocks
            List<Block> blocksToRemove = new List<Block>();

            foreach (Block b in world.Blocks)
            {
                if (b.XCoordinate < world.WorldXCoordinate && b.XCoordinate + b.Width < world.WorldXCoordinate)
                {
                    blocksToRemove.Add(b);
                }

                if (b.XCoordinate < world.WorldXCoordinate && b.XCoordinate + b.Width < world.WorldXCoordinate && b.HasBeenDrawn)
                {
                    blocksToReturn.Add(b);
                }
            }

            blocksToRemove.ForEach(b => world.Blocks.Remove(b));

            return blocksToReturn;
        }
        public static List<Block> ReturnUpdatedBlocks(World world)
        {
            List<Block> blocksToReturn = new List<Block>();

            foreach (Block b in world.Blocks)
            {

                if (b.XCoordinate > world.WorldXCoordinate && b.XCoordinate < world.WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
                else if (b.XCoordinate + b.Width > world.WorldXCoordinate && b.XCoordinate + b.Width < world.WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
                // TODO: Fix this thing
                else if (b.XCoordinate > world.WorldXCoordinate && b.XCoordinate < world.WorldXCoordinate + GameInfo.SCREEN_WIDTH && !string.IsNullOrEmpty(b.FileName)
                    && !b.HasBeenDrawn)
                {
                    blocksToReturn.Add(b);
                }
            }

            return blocksToReturn;
        }
        public static void UpdateWorld(World world)
        {

            List<Block> blocks = world.Blocks;

            List<Block>? luckyBlocks = blocks.Where(b => b.FileName == "LuckyBlock" ||
                                                b.FileName == "LuckyBlockGlow" ||
                                                b.FileName == "LuckyBlockGlowGlow").ToList();

            Block? bumbedBlock = blocks.FirstOrDefault(b => b.PlayerHasInteracted);


            // TODO: Export in a new separate file
            if (!_isUpdatingLuckyBlocks && luckyBlocks is not null)
            {
                Thread updateThread = new Thread(() => UpdateLuckyBlocks(luckyBlocks));

                updateThread.Start();
            }

            if (bumbedBlock is not null)
            {
                OnInteractedBlock(bumbedBlock);
            }

            if (_movementTask is not null)
            {
                _movementTask.Execute();

                if (_movementTask.IsFulfilled)
                {
                    _movementTask = null;
                }
            }

        }
        private static void UpdateLuckyBlocks(List<Block> luckyBlocks)
        {
            _isUpdatingLuckyBlocks = true;

            Thread.Sleep(250);

            foreach (Block block in luckyBlocks)
            {
                switch (block.FileName)
                {
                    case "LuckyBlock":
                        block.FileName = "LuckyBlockGlow";
                        break;
                    case "LuckyBlockGlow":
                        block.FileName = "LuckyBlockGlowGlow";
                        break;
                    case "LuckyBlockGlowGlow":
                        block.FileName = "LuckyBlock";
                        break;
                }

                block.NeedsToBeUpdated = true;
            }

            _isUpdatingLuckyBlocks = false;
        }
        private static void OnInteractedBlock(Block block)
        {
            if (block.FileName.Contains("Lucky"))
            {
                block.FileName = "Blank";

                block.NeedsToBeUpdated = true;

                _movementTask = new MovementTask(block, 0, 1, block.YCoordinate + 4);

            }
            else if (block.FileName == "Brick")
            {
                _movementTask = new MovementTask(block, 0, 1, block.YCoordinate + 4);
            }

            block.PlayerHasInteracted = false;
        }

        #endregion Update World

        #region Populate World Functions
        private static void PopulateWorldObject(World world, int worldID)
        {
            List<WorldEntity> entities = _entities.Where(e => e.WorldID == worldID).ToList();

            List<Block> blocks = new List<Block>();
            List<Enemy> enemies = new List<Enemy>();

            foreach (WorldEntity e in entities)
            {
                if (e is Block)
                {
                    Block block = new Block(e.FileName, e.WorldID, e.XCoordinate, e.YCoordinate, e.HorizontalSpeed, e.VerticalSpeed, e.Width, e.Height);

                    blocks.Add(block);
                }

                if (e is Enemy)
                {
                    enemies.Add(new Enemy(e.FileName, e.WorldID, e.XCoordinate, e.YCoordinate, e.HorizontalSpeed, e.VerticalSpeed, e.Width, e.Height));
                }
            }

            world.Blocks = blocks;
            world.Enemies = enemies;
        }
        private static void AddNewEnemy(string fileName, int mapID, double xCoordinate, double yCoordinate,
                                              int width, int height, double horizontalSpeed = 0, double verticalSpeed = 0)
        {
            _entities.Add(new Enemy(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height));
        }
        private static void AddNewBlock(string fileName, int mapID, double xCoordinate, double yCoordinate,
                                              int width, int height, double horizontalSpeed = 0, double verticalSpeed = 0)
        {
            _entities.Add(new Block(fileName, mapID, xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height));
        }
        #endregion Populate World Functions
    }
}
