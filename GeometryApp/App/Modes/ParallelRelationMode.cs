using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryApp.App.Modes
{
    class ParallelRelationMode : IMode
    {
        private bool _finalized = false;
        private Board _board;

        private Shapes.Polygon _polygon;
        private int _pointIndex1 = -1;
        private int _pointIndex2;
        int cnt = 0;


        public bool Finalized
        {
            get
            {
                return _finalized;
            }
        }

        public ParallelRelationMode(Board board)
        {
            _board = board;
        }

        public void HandleClick(Signal signal)
        {
            if (signal.type == Type.Down)
            {
                clickFunc(signal);
            }
        }

        void clickFunc(Signal signal)
        {
            int x = signal.mouseEvent.X; int y = signal.mouseEvent.Y;
            var res = _board.FindEdge(x, y);
            if (res.pointIndex == -1 ||
                (_polygon != null &&
                (_polygon != res.polygon || res.pointIndex == _pointIndex1)) ||
                (_pointIndex1 != -1 && (res.pointIndex == _polygon.NextInd(_pointIndex1) || res.pointIndex == _polygon.PrevInd(_pointIndex1))) ||
                res.polygon.points[res.pointIndex].relation != Shapes.Relation.Empty) return;

            cnt++;

            if (cnt == 1)
            {
                _polygon = res.polygon;
                _pointIndex1 = res.pointIndex;
            }
            if (cnt == 2)
            {
                _pointIndex2 = res.pointIndex;

                _board.SetRelation(_polygon, Shapes.Relation.Parallel, _pointIndex1, _pointIndex2);

                _finalized = true;
            }
        }
    }
}
