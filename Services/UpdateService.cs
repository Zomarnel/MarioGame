using Models;

namespace Services
{
    public static class UpdateService
    {
        private static List<ITask> _tasksQueue = new List<ITask>();

        private static bool _isUpdatingLuckyBlocks = false; 
        public static void UpdateBlocks()
        {
            if (!_isUpdatingLuckyBlocks)
            {
                if (_tasksQueue.Any(t => t is GlowingTask))
                {
                    UpdateLuckyBlocksAsync();
                }
            }

            if (_tasksQueue.Any(t => t is MovementTask))
            {
                MovementTask movTask = (MovementTask)_tasksQueue.First(t => t is MovementTask);

                movTask.Execute();

                if (movTask.IsFulfilled)
                {
                    _tasksQueue.Remove(movTask);
                }

            }
        }
        public static void CreateGlowingTasks(List<Block> blocks)
        {
            if (!_isUpdatingLuckyBlocks)
            {
                foreach (Block block in blocks)
                {
                    _tasksQueue.Add(new GlowingTask(block));
                }
            }
        }
        public static void CreateMovementTask(Block block)
        {
            block.PlayerHasInteracted = false;

            _tasksQueue.Add(new MovementTask(block, 0, 1, block.YCoordinate + 4));
        }

        private static async Task UpdateLuckyBlocksAsync()
        {
            _isUpdatingLuckyBlocks = true;

            await Task.Delay(250);

            foreach (ITask task in _tasksQueue.Where(t => t is GlowingTask))
            {
                task.Execute();
            }

            _tasksQueue.RemoveAll(t => t is GlowingTask);

            _isUpdatingLuckyBlocks = false;
        }
    }
}
