using Core;
using Models;

namespace Services
{
    public static class Boundaries
    {
        private static List<Boundary> _boundaries = new List<Boundary>();
        static Boundaries()
        {
            _boundaries.Add(new Boundary(0, 0, 992, 72));
        }
        public static void IsPlayerInsideBoundaries(Player player)
        {
            if (player.XCoordinate < 0)
            {
                player.HorizontalAction = Player.HorizontalActions.IsStanding;
                player.HorizontalSpeed = 0;

                player.XCoordinate = 0;
            }
            else if (player.XCoordinate > GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH)
            {
                player.HorizontalAction = Player.HorizontalActions.IsStanding;
                player.HorizontalSpeed = 0;

                player.XCoordinate = GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH;
            }

            foreach (Boundary bnd in _boundaries)
            {
                if (player.XCoordinate >= bnd.XStart && player.XCoordinate <= bnd.XEnd)
                {
                    if (player.YCoordinate > bnd.YStart && player.YCoordinate < bnd.YEnd)
                    {
                        if (player.VerticalSpeed > 0)
                        {
                            player.YCoordinate = bnd.YStart;
                            player.VerticalSpeed = -GameInfo.GAME_GRAVITY;

                            player.VerticalAction = Player.VerticalActions.IsStanding;
                        }
                        else if (player.VerticalSpeed < 0)
                        {
                            player.YCoordinate = bnd.YEnd;
                            player.VerticalSpeed = 0;

                            player.VerticalAction = Player.VerticalActions.IsStanding;
                        }
                    }
                }
            }
        }
    }
}
