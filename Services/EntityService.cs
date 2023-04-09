using Core;
using Models;

namespace Services
{
    public static class EntityService
    {

        private static List<Block> _blocks = new List<Block>();
        public static void UpdateEnemies(List<Enemy> enemies, List<Block> blocks)
        {
            _blocks = blocks;

            foreach (Enemy enemy in enemies)
            {
                MoveEnemyXCoordinate(enemy);
                MoveEnemyYCoordinate(enemy);
            }

            UpdateService.UpdateMobsSprite(enemies);
        }
        public static void OnEnemyDeath(Enemy enemy, Player? player, string deathAnimation = "")
        {
            if (player is not null)
            {
                player.CurrentSpriteID = 1;
                player.HasKilledEnemyCooldown = true;

                Movement.OnJump(player);
            }

            enemy.HasBeenKilled = true;

            if (deathAnimation == "crushed")
            {
                enemy.HorizontalSpeed = 0;
                enemy.VerticalSpeed = 0;

                if (enemy.FileName == "Mushroom")
                {
                    enemy.SpriteID = 52;

                    enemy.EnemyDeathCooldown();
                }
            }
            else if (deathAnimation == "fall")
            {
                enemy.InitialY = enemy.YCoordinate;
                enemy.HorizontalSpeed = 0;

                enemy.SpriteID = -enemy.SpriteID;
            }

            /*
            switch (enemy.FileName)
            {
                case "Mushroom":
                    enemy.SpriteID = 52;
                    enemy.HorizontalSpeed = 0;

                    enemy.EnemyDeathCooldown();

                    break;

                case "Turtle":
                    enemy.SpriteID = 62;
                    enemy.Height = 28;
                    break;
            }*/
        }

        public static void OnPlayerDeath(Player player)
        {
            player.IsDead = true;
            player.CurrentSpriteID = 8;

            player.HorizontalSpeed = 0;
            player.VerticalSpeed = 0;

            Movement.OnJump(player);
        }

        public static void OnPlayerInteractedBlock(Block block, List<Enemy> enemies)
        {
            // TODO: IF LUCKYBLOCK SPAWN ITEM

            // Check if enemy was on top of the block

            foreach (Enemy enemy in enemies)
            {
                if (enemy.YCoordinate == block.YCoordinate + block.Height)
                {
                    if ((enemy.XCoordinate >= block.XCoordinate && enemy.XCoordinate <= block.XCoordinate + block.Width) ||
                        (enemy.XCoordinate + enemy.Width >= block.XCoordinate && enemy.XCoordinate + enemy.Width <= block.XCoordinate + block.Width))
                    {
                        OnEnemyDeath(enemy, null, "fall");
                    }

                }
            }
        }

        private static void MoveEnemyXCoordinate(Enemy enemy)
        {
            if (enemy.HasBeenKilled)
            {
                return;
            }

            enemy.XCoordinate += enemy.HorizontalSpeed;

            Collisions.HorizontalEnemyBoundariesCheck(enemy, _blocks);
        }

        private static void MoveEnemyYCoordinate(Enemy enemy)
        {
            // Case: Mushroom was killed on top of a block, was killed by turtle shell, or by fireball
            // Case: Turtle was killed by fireball

            if (enemy.HasBeenKilled)
            {
                if (enemy.SpriteID < 0)
                {
                    if (enemy.VerticalSpeed == 0)
                    {
                        enemy.InitialY = enemy.YCoordinate;
                    }

                    enemy.VerticalSpeed = Movement.CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED / 10, GameInfo.GAME_GRAVITY,
                                                                    enemy.YCoordinate, enemy.InitialY, true);

                    enemy.YCoordinate += enemy.VerticalSpeed;
                }

                return;
            }

            // Enemy is falling
            if (enemy.VerticalSpeed < 0)
            {
                enemy.VerticalSpeed = Movement.CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED, GameInfo.GAME_GRAVITY,
                                                                    enemy.YCoordinate, enemy.InitialY, true);
            }

            enemy.YCoordinate += enemy.VerticalSpeed;

            Collisions.VerticalEnemyBoundariesCheck(enemy, _blocks);
        }
    }
}
