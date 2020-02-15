using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GeometryApp.App.Modes;

namespace GeometryApp.App
{
    class GeoApp
    { 
        Board _board = new Board();
        Bitmap _bitmap;
        PictureBox _pictureBox;
        Mode eMode;
        IMode _mode;

        public GeoApp(PictureBox pictureBox, Bitmap bitmap, Label label)
        {
            _pictureBox = pictureBox;
            _pictureBox.Image = bitmap;
            _bitmap = bitmap;
            _board.polygons.Add(new Shapes.Polygon());
            _board.setLabel(label);

            eMode = Mode.NewPolygon;
            _mode = new NewPolygonMode(_board);
        }

        public void ChangeMode(Mode mode)
        {
            eMode = mode;
            _mode = ModeFactory(mode);
        }

        public void HandleClick(Signal signal)
        {
            if(!_mode.Finalized)
                _mode.HandleClick(signal);
            else
                _mode = ModeFactory(eMode);

            Drawer.Redraw(_bitmap, _board.polygons, _pictureBox);
        }

        private IMode ModeFactory(Mode mode)
        {
            IMode result = null;
            switch (mode)
            {
                case Mode.MovePoint:
                    result = new MoveVertexMode(_board);
                    break;
                case Mode.MovePolygon:
                    result = new MovePolygonMode(_board);
                    break;
                case Mode.NewPolygon:
                    result = new NewPolygonMode(_board);
                    break;
                case Mode.EqualRelation:
                    result = new EqualRelationMode(_board);
                    break;
                case Mode.ParallelRelation:
                    result = new ParallelRelationMode(_board);
                    break;
                case Mode.DivideEdge:
                    result = new DivideEdgeMode(_board);
                    break;
                case Mode.RemoveVertex:
                    result = new DeleteVertexMode(_board);
                    break;
                case Mode.RemoveRelation:
                    result = new RemoveRelationMode(_board);
                    break;
                case Mode.RemovePolygon:
                    result = new RemovePolygonMode(_board);
                    break;
            }
            return result;
        }

    }
}
