using Core;
using Models;

namespace Services
{
    public static class WorldEntitiesControl
    {
        private static List<MovementTask> _movementTasks = new List<MovementTask>(); 

        private static bool IsUpdatingLuckyBlocks = false;
        public static void UpdateWorldBlocks(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                if (block.XCoordinate >= Math.Abs(MapService.MapXCoordinate)
                    && block.XCoordinate + block.Width <= Math.Abs(MapService.MapXCoordinate) + GameInfo.SCREEN_WIDTH
                    && !string.IsNullOrEmpty(block.FileName)
                    && !block.HasBeenDrawn)
                {
                    block.NeedsToBeUpdated = true;
                }
            }

            List<Block> luckyBlocks = blocks.Where(b => b.FileName == "LuckyBlock" ||
                                                        b.FileName == "LuckyBlockGlow" ||
                                                        b.FileName == "LuckyBlockGlowGlow").ToList();

            List<Block> bumpedBricks = blocks.Where(b => b.FileName == "Brick" && b.PlayerHasBumped).ToList();

            if (!IsUpdatingLuckyBlocks)
            {
                IsUpdatingLuckyBlocks = true;
                UpdateLuckyBlocksAsync(luckyBlocks);
            }

            if (bumpedBricks.Count > 0)
            {
                bumpedBricks.ForEach(b => _movementTasks.Add(new MovementTask(b, 0, 1, b.YCoordinate + 4)));

                bumpedBricks.ForEach(b => b.PlayerHasBumped = false);
            }

            if (_movementTasks.Count != 0)
            {
                RunMovementTasks();
            }
        }
        private static void RunMovementTasks()
        {
            List<MovementTask> movementTasks = new List<MovementTask>(_movementTasks);


            foreach (MovementTask task in movementTasks)
            {
                task.Execute();

                if (task.IsFulfilled)
                {
                    _movementTasks.Remove(task);
                }
            }
        }
        private static async Task UpdateLuckyBlocksAsync(List<Block> luckyBlocks)
        {
            await Task.Delay(250);

            if (luckyBlocks.Any(b => b.FileName == "LuckyBlock"))
            {
                luckyBlocks.ForEach(b => b.FileName = "LuckyBlockGlow");

                luckyBlocks.ForEach(b => b.NeedsToBeUpdated = true);
            }
            else if (luckyBlocks.Any(b => b.FileName == "LuckyBlockGlow"))
            {
                luckyBlocks.ForEach(b => b.FileName = "LuckyBlockGlowGlow");

                luckyBlocks.ForEach(b => b.NeedsToBeUpdated = true);
            }
            else
            {
                luckyBlocks.ForEach(b => b.FileName = "LuckyBlock");

                luckyBlocks.ForEach(b => b.NeedsToBeUpdated = true);
            }

            IsUpdatingLuckyBlocks = false;
        }
    }
}
