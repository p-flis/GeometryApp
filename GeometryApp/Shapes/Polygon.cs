using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryApp.Shapes
{
    class Polygon
    {
        public List<Point> points = new List<Point>();

        public int Count
        {
            get
            {
                return points.Count;
            }
        }

        public Point Next(int point)
        {
            return points[(point + 1) % Count];
        }
        public Point Prev(int point)
        {
            return points[(point - 1 + Count) % Count];
        }
        public Point Curr(int point)
        {
            return points[point];
        }
        public int NextInd(int point)
        {
            return (point + 1) % Count;
        }
        public int PrevInd(int point)
        {
            return (point - 1 + Count) % Count;
        }
    }
}
