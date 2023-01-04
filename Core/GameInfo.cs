namespace Core
{
    public static class GameInfo
    {
        public static readonly double SCREEN_WIDTH = 1024;

        public static readonly double SCREEN_HEIGHT = 584;

        public static readonly double SPRITE_WIDTH = 32;

        public static readonly double SPRITE_HEIGHT = 32;

        public static readonly double GAME_GRAVITY = 0.125;

        public static readonly double PLAYER_HORIZONTAL_MIN_SPEED = 0.5;

        public static readonly double PLAYER_HORIZONTAL_MAX_SPEED = 5.5;

        public static readonly double PLAYER_HORIZONTAL_ACCELERATION_SPEED = 0.05;

        public static readonly double PLAYER_HORIZONTAL_DECELERATION_SPEED = 0.1;

        public static readonly double PLAYER_VERTICAL_SPEED = Math.Sqrt(32); //5.5 // sqrt(32)

    }
}
