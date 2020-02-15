using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp.App.Modes
{
    class NewPolygonMode : IMode
    {
        Board _board;
        Shapes.Polygon _polygon;
        bool _finalized = false;

        public bool Finalized
        {
            get
            {
                return _finalized;
            }
        }


        public NewPolygonMode(Board board)
        {
            _board = board;
            _polygon = new Shapes.Polygon();
            _board.polygons.Add(_polygon);
        }

        public void HandleClick(Signal signal)
        {
            if(signal.type == Type.Move)
            {
                MoveFunc(signal);
            }
            else if (signal.type == Type.Down)
            {
                if (signal.mouseEvent.Button == MouseButtons.Left)
                    ClickFunc(signal);
                else
                {   if(_polygon.Count > 2)
                        _finalized = true;
                }

            }
        }

        void MoveFunc(Signal signal)
        {
            if(_polygon.Count > 1)
            {
                _polygon.points[_polygon.Count - 1].X = signal.mouseEvent.X;
                _polygon.points[_polygon.Count - 1].Y = signal.mouseEvent.Y;
            }
        }

        void ClickFunc(Signal signal)
        {
            int x = signal.mouseEvent.X;
            int y = signal.mouseEvent.Y;
            var p = new Shapes.Point { X = x, Y = y, R = 5};
            _polygon.points.Add(p);       
        }
    }
}
