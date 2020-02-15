using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp.App.Modes
{
    class DeleteVertexMode : IMode
    {
        private bool _finalized = false;
        private Board _board;

        public bool Finalized
        {
            get
            {
                return _finalized;
            }
        }

        public DeleteVertexMode(Board board)
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
            }
        }

        public void ClickFunc(Signal signal)
        {
            var res = _board.FindPoint(signal.mouseEvent.X, signal.mouseEvent.Y);
            if (res.polygon != null)
            {
                if (res.polygon.Count <= 3) return;
                _board.RemoveVertex(res.polygon, res.pointIndex);
                _finalized = true;
            }
        }
    }
}
