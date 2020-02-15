using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp.App.Modes
{
    class MoveVertexMode : IMode
    {
        private bool _finalized = false;
        private Board _board;
        private Shapes.Polygon _polygon;
        private int _pointIndex;


        //edge case
        bool _edge = false;
        int xOffset = 0;
        int yOffset = 0;


        public bool Finalized
        {
            get
            {
                return _finalized;
            }
        }

        public MoveVertexMode(Board board)
        {
            _board = board;
        }

        public void HandleClick(Signal signal)
        {
            if (signal.mouseEvent.Button == MouseButtons.Left)
            {
                if (signal.type == Type.Down)
                {
                    ClickFunc(signal);
                }
                else if (signal.type == Type.Move && _polygon != null)
                {
                    MoveFunc(signal);
                }
            }
            else if (_polygon != null)
            {
                _finalized = true;
            }
        }

        public void ClickFunc(Signal signal)
        {
            int x = signal.mouseEvent.X; int y = signal.mouseEvent.Y;
            var res = _board.FindPoint(x, y);

            if (res.pointIndex == -1)
            {
                res = _board.FindEdge(x, y);
                if (res.pointIndex == -1) return;
                _edge = true;
                xOffset = x - res.polygon.points[res.pointIndex].X;
                yOffset = y - res.polygon.points[res.pointIndex].Y;
            }

            _polygon = res.polygon;
            _pointIndex = res.pointIndex;
        }

        public void MoveFunc(Signal signal)
        {
            int x = signal.mouseEvent.X; int y = signal.mouseEvent.Y;
            int dx = x - _polygon.points[_pointIndex].X-xOffset; int dy = y - _polygon.points[_pointIndex].Y-yOffset;
            if (_edge) _board.MoveEdge(_polygon, _pointIndex, dx, dy);
            else _board.MovePoint(_polygon, _pointIndex, dx, dy);
        }
    }
}
