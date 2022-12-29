﻿
namespace Models
{
    public class Rectangle
    {
        public readonly Point[] Corners = new Point[4];
        public int Width { get; set; }
        public int Height { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public Rectangle(int width, int height, double x, double y)
        {
            Width = width;
            Height = height;

            X = x;

            Y = y;

            Corners[0] = new Point(X, Y, Point.Location.BottomLeft);
            Corners[1] = new Point(X + Width, Y, Point.Location.BottomRight);
            Corners[2] = new Point(X + Width, Y + Height, Point.Location.TopRight);
            Corners[3] = new Point(X, Y + Height, Point.Location.TopLeft);
        }
        public static Rectangle ConvertEntityToRectangle(Entity entity)
        {
            return new Rectangle(entity.Width, entity.Height, entity.XCoordinate, entity.YCoordinate);
        }
        public static Rectangle? Intersect(Rectangle rect1, Rectangle rect2)
        {
            List<Rectangle> commonRectangles = new List<Rectangle>();

            // Checks for special cases as such as when: 
            // The widths of the two rectangles are equal

            if (rect1.X == rect2.X && rect1.Width == rect2.Width) 
            {
                for (int i = 0; i < 4; i++)
                {
                    Point point = rect1.Corners[i];

                    if (point.Y > rect2.Y && point.Y < rect2.Y + rect2.Height)
                    {   
                        // TODO: Fix this
                        commonRectangles.Add(new Rectangle(rect1.Width, Math.Abs(rect1.Height - rect2.Height), rect2.X, rect2.Y));
                        break;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Point point = rect1.Corners[i];

                if (point.X > rect2.X && point.X < rect2.X + rect2.Width && point.Y > rect2.Y && point.Y < rect2.Y + rect2.Height)
                {
                    int index = (i + 2) % 4;

                    int width = Math.Abs((int)(rect2.Corners[index].X - point.X));
                    int height = Math.Abs((int)(rect2.Corners[index].Y - point.Y));

                    double x = 0;
                    double y = 0;

                    switch (point.PointLocation)
                    {
                        case Point.Location.BottomLeft:
                            x = point.X;
                            y = point.Y;
                            break;

                        case Point.Location.BottomRight:
                            x = point.X - width;
                            y = point.Y;
                            break;

                        case Point.Location.TopRight:
                            x = point.X - width;
                            y = point.Y - height;
                            break;

                        case Point.Location.TopLeft:
                            x = point.X;
                            y = point.Y - height;
                            break;
                    }

                    commonRectangles.Add(new Rectangle(width, height, x, y));
                }
            }

            if (commonRectangles.Count == 0)
            {
                return null;
            }

            if (commonRectangles.Count == 1)
            {
                return commonRectangles.First();
            }

            if ((commonRectangles[0].Width + commonRectangles[0].Height) > (commonRectangles[1].Width + commonRectangles[1].Height))
            {
                return commonRectangles[0];
            }
            else
            {
                return commonRectangles[1];
            }

        }
    }
}
