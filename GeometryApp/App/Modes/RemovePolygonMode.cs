using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp.App.Modes
{
    class RemovePolygonMode : IMode
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

        public RemovePolygonMode(Board board)
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
            var res = _board.FindEdge(signal.mouseEvent.X, signal.mouseEvent.Y);
            if (res.polygon != null)
            {
                _board.RemovePolygon(res.polygon);
                _finalized = true;
            }
        }
    }
}
