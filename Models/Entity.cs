
namespace Models
{
    public class Entity
    {
        public double XCoordinate
        {
            get => Math.Round(_xCoordinate, 2);
            set
            {
                _xCoordinate = value;
            }
        }
        public double YCoordinate
        {
            get => Math.Round(_yCoordinate, 2);
            set
            {
                _yCoordinate = value;
            }
        }

        public double HorizontalSpeed
        {
            get => Math.Round(_horizontalSpeed, 2);
            set
            {
                _horizontalSpeed = value;
            }
        }
        public double VerticalSpeed
        {
            get => Math.Round(_verticalSpeed, 2);
            set
            {
                _verticalSpeed = value;
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }

        private double _horizontalSpeed;
        private double _verticalSpeed;

        private double _xCoordinate;
        private double _yCoordinate;
        public Entity(double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;

            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed;

            Width = width;
            Height = height;
        }
    }
}
