using GeometryApp.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp
{
    public partial class Form1 : Form
    {
        Bitmap _bitmap;
        GeoApp geoApp;
        
        public Form1()
        {
            InitializeComponent();
            _bitmap = new Bitmap(picture.Width, picture.Height);
            geoApp = new GeoApp(picture, _bitmap, debugText);
        }

        private void picture_Click(object sender, EventArgs e)
        {
            //var point = picture.PointToClient(Cursor.Position);
            //Signal signal = new Signal { X = point.X, Y = point.Y };
            //geoApp.HandleClick(signal);
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            debugText.Text = $"{e.X} {e.Y}";

            var sig = new Signal();
            sig.mouseEvent = e;
            sig.type = App.Type.Down;

            geoApp.HandleClick(sig);
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            var sig = new Signal();
            sig.mouseEvent = e;
            sig.type = App.Type.Move;

            geoApp.HandleClick(sig);
        }

        private void movePolygonButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.MovePolygon);
        }

        private void newPolygonButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.NewPolygon);
        }

        private void movePointButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.MovePoint);
        }

        private void equalRelationButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.EqualRelation);
        }

        private void parallelRelationButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.ParallelRelation);
        }

        private void divideEdgeButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.DivideEdge);
        }

        private void removeVertexButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.RemoveVertex);
        }

        private void removeRelationButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.RemoveRelation);
        }

        private void removePolygonButton_MouseDown(object sender, MouseEventArgs e)
        {
            geoApp.ChangeMode(App.Modes.Mode.RemovePolygon);
        }
    }
}
