
namespace Models
{
    public class MovementTask
    {
        public WorldEntity WorldEntity { get; set; }
        public double HorizontalSpeed { get; set; }
        public double VerticalSpeed { get; set; }
        public double VerticalLimit { get; set; }
        public bool IsFulfilled { get; set; } = false;

        private double _oldXCoordinate;

        private double _oldYCoordinate;
        public MovementTask(WorldEntity worldEntity, double horizontalSpeed, double verticalSpeed, double verticalLimit)
        {
            WorldEntity = worldEntity;
            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed;
            VerticalLimit = verticalLimit;

            _oldXCoordinate = worldEntity.XCoordinate;
            _oldYCoordinate = worldEntity.YCoordinate;
        }
        public void Execute()
        {
            WorldEntity.XCoordinate += HorizontalSpeed;
            WorldEntity.YCoordinate += VerticalSpeed;

            if (WorldEntity.YCoordinate >= VerticalLimit)
            {
                WorldEntity.YCoordinate = VerticalLimit;

                VerticalSpeed = -VerticalSpeed;
            }

            if (WorldEntity.YCoordinate <= _oldYCoordinate)
            {
                WorldEntity.YCoordinate = _oldYCoordinate;

                IsFulfilled = true;
            }

            WorldEntity.NeedsToBeUpdated = true;
        }
    }
}
