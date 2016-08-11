using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerMisc
{
    public class Point
    {
        public double x, y, z;
    }

    public class Line
    {
        public Point p1, p2;
        public double Slope
        {
            get
            {
                return (p2.y - p1.y) / (p2.x - p1.x);
            }
        }
        public double GetY(double x)
        {
            return this.Slope * (x - p1.x) + p1.y;
        }
        static public double GetY(double x,double slope,Point p)
        {
            return slope*(x-p.x)+p.y;
        }
        public Point Intersection(Line l)
        {
            double m1 = this.Slope, m2 = l.Slope, x1 = p1.x, x2 = l.p1.x, y1 = p1.y, y2 = l.p1.y;
            if (m1 == m2)
                return null;
            double intersectX = (m1 * x1 - y1 - m2 * x2 + y2) / (m1 - m2);
            double intersectY = GetY(intersectX);
            return new Point() { x = intersectX, y = intersectY, z = 0 };
        }

        public Line GetPerpendicularThroughPoint(Point p)
        {
            double m = getPerpendicularSlope();
            Line perp = new Line()
            {
                p1 = p,
                p2 = new Point()
                {
                    x = 0,
                    y = GetY(0, m, p)
                }
            };
            Point intpoint = Intersection(perp);
            perp.p2 = intpoint;
            return perp;         

        }

        public double GetLength(double x1,double x2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow(GetY(x1) - GetY(x2),2));
        }
        
        public double GetLength()
        {
            return GetLength(p1.x, p2.x);
        }

        private double getPerpendicularSlope()
        {
            return -1 / Slope;
        }
    }

    public class Triangle:Shape
    {
        public Triangle(Point p1,Point p2,Point p3)
        {
            myVertices.Add(p1);
            myVertices.Add(p2);
            myVertices.Add(p3);
        }
        public override double GetArea()
        {
            Line l = new Line() { p1 = myVertices[0], p2 = myVertices[1] };
            Line l2 = l.GetPerpendicularThroughPoint(myVertices[2]);
            return l.GetLength() * l2.GetLength() / 2;
        }
    }
}
