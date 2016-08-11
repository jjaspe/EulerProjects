using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerMisc
{
    public class Shape
    {
        protected List<Point> myVertices=new List<Point>();

        public virtual double GetArea()
        {
            Point v1=myVertices[0];
            double area = 0;

            for (int i = 1; i < myVertices.Count; i++)
            {
                Point v2 = myVertices[i], v3 = myVertices[i + 1];
                Triangle t = new Triangle(v1, v2, v3);
                area += t.GetArea();
            }
            return area;
        }

        public bool IsConvex()
        {
            List<Line> edgeLines = new List<Line>();
            for (int i = 0; i < myVertices.Count-1; i++)
            {
                edgeLines.Add(new Line() { p1 = myVertices[i], p2 = myVertices[i + 1] });
            }
            edgeLines.Add(new Line() { p1 = myVertices[myVertices.Count - 1], p2 = myVertices[0] });

            return !innerLinesIntersectEdgeLines(edgeLines);
        }

        bool innerLinesIntersectEdgeLines(List<Line> edgeLines)
        {
            for (int j = 0; j < myVertices.Count; j++)
            {
                Point v1 = myVertices[j];
                for (int i = 2; i < myVertices.Count-1; i++)
                {
                    Line innerLine = new Line() { p1 = v1, p2 = myVertices[(i+j)%myVertices.Count] };
                    foreach(Line edge in edgeLines)
                    {
                        Point p = innerLine.Intersection(edge);
                        if (!p.Equals(myVertices[(i + j) % myVertices.Count]))
                            return true;
                    }
                }
            }
            
            return false;
        }
    }
}
