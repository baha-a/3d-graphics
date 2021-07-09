using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicProject
{
    public class Point
    {
        public double X { get; set; }
        public double Z { get; set; }
        public Point(double x = 0, double z = 0) { X = x; Z = z; }

        public static bool operator !=(Point p1, Point p2)
        {
            return (p1.X != p2.X || p1.Z != p2.Z);
        }
        public static bool operator ==(Point p1, Point p2)
        {
            return (p1.X == p2.X && p1.Z == p2.Z);
        }

        public static Point operator -(Point first, Point other)
        {
            return new Point(first.X - other.X, first.Z - other.Z);
        }

        public static double Distance(double px, double pz, double tx, double tz)
        {
            double dx = px - tx, dz = pz - tz;
            return Math.Sqrt((dx * dx + dz * dz));
        }
        public double DistanceFrom(Point p)
        {
            double dx = p.X - X, dz = p.Z - Z;
            return Math.Sqrt((dx * dx + dz * dz));
        }

        public Point MidPointWith(Point p)
        {
            return new Point((p.X + X) / 2, (p.Z + Z) / 2);
        }
        public Point moveTo(Point p)
        {
            double  dx = p.X - X, dz = p.Z - Z , distance = Math.Sqrt((dx * dx + dz * dz));
            if (distance > Train.Speed)
                return new Point(X + Train.Speed / distance * dx, Z + Train.Speed / distance * dz);
            return p;
        }
        public Point moveTo(Point p, int speed)
        {
            double  dx = p.X - X, dz = p.Z - Z , distance = Math.Sqrt((dx * dx + dz * dz));
            if (distance > speed)
                return new Point(X + speed / distance * dx, Z + speed / distance * dz);
            return p;
        }

        public static double rotateTo(double angle, double angleTo, double speed)
        {
            if (angle < angleTo && speed + angle < angleTo)
                return speed + angle;
            else if (angle > angleTo && angle - speed > angleTo)
                return angle - speed;
            return angleTo;
        }
    }
}
