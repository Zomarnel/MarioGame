using Models;

namespace Services
{
    public static class UpdateService
    {
        #region Player Sprite Update

        private static int _playerSpriteUpdateTime = 100;

        private static bool _isPlayerUpdating = false;
        public static void UpdatePlayerSprite(Player player)
        {
            if (player.CurrentSpriteID > 0)
            {
                if (player.HorizontalSpeed >= 0)
                {
                    if (player.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        if (player.HasKilledEnemyCooldown)
                        {
                            player.CurrentSpriteID = 1;
                            return;
                        }

                        if (player.IsDead)
                        {
                            player.CurrentSpriteID = 8;
                            return;
                        }

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
                        if (!_isPlayerUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {
                            UpdatePlayerSpriteRunningAsync(player);
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
                        if (player.HasKilledEnemyCooldown)
                        {
                            player.CurrentSpriteID = -1;
                            return;
                        }

                        if (player.IsDead)
                        {
                            player.CurrentSpriteID = 8;
                            return;
                        }

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
                        if (!_isPlayerUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {

                            UpdatePlayerSpriteRunningAsync(player);

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
            _isPlayerUpdating = true;

            await Task.Delay(_playerSpriteUpdateTime);

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

            _isPlayerUpdating = false;
        }

        #endregion Player Sprite Update

        #region Enemy Sprite Update
        private static int _enemySpriteUpdateTime = 250;
        private static bool _isMobsUpdating = false;

        public static void UpdateMobsSprite(List<Enemy> enemies)
        {
            void UpdateSprite()
            {
                _isMobsUpdating = true;

                Thread.Sleep(_enemySpriteUpdateTime);

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.FileName == "Mushroom")
                    {
                        if (!enemy.HasBeenKilled)
                        {
                            enemy.SpriteID += 1;

                            if (enemy.SpriteID > 51)
                            {
                                enemy.SpriteID = 50;
                            }
                        }
                        else if (enemy.SpriteID < 0)
                        {
                            enemy.SpriteID--;

                            if (enemy.SpriteID < -51)
                            {
                                enemy.SpriteID = -50;
                            }
                        }
           
                    }

                    if (enemy.FileName == "Turtle")
                    {
                        if (!enemy.HasBeenKilled)
                        {
                            if (enemy.IsShelled && enemy.HorizontalSpeed != 0)
                            {
                                enemy.SpriteID++;

                                if (enemy.SpriteID > 64)
                                {
                                    enemy.SpriteID = 62;
                                }

                                continue;
                            }

                            if (enemy.HorizontalSpeed > 0)
                            {
                                if (enemy.SpriteID < 0)
                                {
                                    enemy.SpriteID = -enemy.SpriteID;
                                }

                                enemy.SpriteID++;

                                if (enemy.SpriteID > 61)
                                {
                                    enemy.SpriteID = 60;
                                }
                            }
                            else if (enemy.HorizontalSpeed < 0)
                            {
                                if (enemy.SpriteID > 0)
                                {
                                    enemy.SpriteID = -enemy.SpriteID;
                                }

                                enemy.SpriteID--;

                                if (enemy.SpriteID < -61)
                                {
                                    enemy.SpriteID = -60;
                                }
                            }
                        }
                        else if (enemy.SpriteID <= -62)
                        {
                            enemy.SpriteID--;

                            if (enemy.SpriteID < -64)
                            {
                                enemy.SpriteID = -64;
                            }
                        }
                    }

                }

                _isMobsUpdating = false;
            }

            if (!_isMobsUpdating)
            {
                Thread updateThread = new Thread(UpdateSprite);

                updateThread.Start();
            }
        }

        #endregion Enemy Sprite Update

        #region Update World

        private static bool _isUpdatingLuckyBlocks = false;

        private static MovementTask? _movementTask;
        public static void UpdateWorld(World world)
        {
            List<Block> blocks = world.Blocks;

            List<Enemy> enemies = WorldFactory.ReturnVisibleEnemies(world);

            UpdateBlocks(blocks);

            EntityService.UpdateEnemies(enemies, blocks);
        }

        private static void UpdateBlocks(List<Block> blocks)
        {
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
                CreateMovementTask(bumbedBlock);
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
        private static void CreateMovementTask(Block block)
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

        #endregion Update World
    }
}
