using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GeometryApp.Shapes
{
    class Point
    {
        //vertex part
        public int X, Y, R;
        public Color C;

        //edge part
        public Relation relation;
        public int relatedIndex;
        public int number;
    }

    enum Relation {Empty, Parallel, Equal}
}
