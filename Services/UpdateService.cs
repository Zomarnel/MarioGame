using Models;
using System.Linq;

namespace Services
{
    public static class UpdateService
    {
        private static List<ITask> _tasksQueue = new List<ITask>();

        #region World Update
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
        public static async void OnBlockBumbed(Block block)
        {
            if (block.FileName == "Brick")
            {
                CreateMovementTask(block);
            }
            else if (block.FileName == "LuckyBlock" ||
                     block.FileName == "LuckyBlockGlow" ||
                     block.FileName == "LuckyBlockGlowGlow")
            {
                // Preventing an exception
                _tasksQueue.Remove(_tasksQueue.Find(t => t.WorldEntity == block));

                block.FileName = "Blank";

                CreateMovementTask(block);
            }
            else
            {
                block.PlayerHasInteracted = false;
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
        

        #endregion World Update

        #region Player Sprite Update
        private static int SpriteUpdateTime { get; set; } = 100;

        private static bool IsUpdating = false;
        public static void UpdatePlayerSprite(Player player)
        {
            if (player.CurrentSpriteID > 0)
            {
                if (player.HorizontalSpeed >= 0)
                {

                    if (player.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        player.CurrentSpriteID = 4;

                        return;
                    }

                    if (player.HorizontalAction == Player.HorizontalActions.IsStanding)
                    {
                        player.CurrentSpriteID = 1;
                    }
                    else if (player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
                    {
                        player.CurrentSpriteID = 5;
                    }
                    else
                    {
                        if (!IsUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {
                            UpdatePlayerSpriteRunningAsync(player);

                            IsUpdating = true;
                        }
                    }
                }
                else
                {
                    player.CurrentSpriteID = -1;
                }
            }

            if (player.CurrentSpriteID < 0)
            {
                if (player.HorizontalSpeed <= 0)
                {
                    if (player.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        player.CurrentSpriteID = -4;

                        return;
                    }

                    if (player.HorizontalAction == Player.HorizontalActions.IsStanding)
                    {
                        player.CurrentSpriteID = -1;
                    }
                    else if (player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
                    {
                        player.CurrentSpriteID = -5;
                    }
                    else
                    {
                        if (!IsUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {
                            UpdatePlayerSpriteRunningAsync(player);

                            IsUpdating = true;
                        }
                    }
                }
                else
                {
                    player.CurrentSpriteID = 1;
                }
            }
        }
        private static async void UpdatePlayerSpriteRunningAsync(Player player)
        {
            await Task.Delay(SpriteUpdateTime);

            if (player.CurrentSpriteID > 0)
            {
                if (player.CurrentSpriteID < 3)
                {
                    player.CurrentSpriteID++;
                }
                else
                {
                    player.CurrentSpriteID = 1;
                }

            }
            else
            {
                if (player.CurrentSpriteID > -3)
                {
                    player.CurrentSpriteID--;
                }
                else
                {
                    player.CurrentSpriteID = -1;
                }
            }

            IsUpdating = false;
        }

        #endregion Player Sprite Update
    }
}
