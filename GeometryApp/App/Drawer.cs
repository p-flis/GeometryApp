using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace GeometryApp.App
{
    static class Drawer
    {
        public static void Redraw(Bitmap bitmap, List<Shapes.Polygon> newPolygons, PictureBox pictureBox)
        {
            Clear(bitmap, pictureBox);
            Draw(bitmap, newPolygons, pictureBox);
        }

        public static void Clear(Bitmap bitmap, PictureBox pictureBox)
        {
            using (Graphics G = Graphics.FromImage(bitmap))
            {
                G.Clear(Color.White);
            }
        }

        static void Draw(Bitmap bitmap, List<Shapes.Polygon> polygons, PictureBox pictureBox)
        {
            Draw(bitmap, polygons);
            pictureBox.Refresh();
        }

        static void Draw(Bitmap bitmap, List<Shapes.Polygon> polygons)
        {
            using (Graphics G = Graphics.FromImage(bitmap))
            {
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                foreach (var poly in polygons)
                {
                    int size = poly.Count;
                    for (int i = 0; i < size; i++)
                    {
                        var p = poly.points[i];

                        G.FillEllipse(Brushes.Blue, p.X - p.R, p.Y - p.R, p.R * 2, p.R * 2);

                        var pn = poly.points[(i + 1) % size];
                        var line = new Shapes.Edge();
                        line.X1 = p.X;
                        line.Y1 = p.Y;
                        line.X2 = pn.X;
                        line.Y2 = pn.Y;
                        DrawText(G, line, p.number, p.relation);
                        Draw2(G, line);

                        //Draw(G, line, p.relation, p.number);
                    }
                }
                G.Flush();
            }

        }

        static void Swap(ref int x, ref int y)
        {
            int tmp = x;
            x = y;
            y = tmp;
        }

        static void DrawText(Graphics g, Shapes.Edge line, int count, Shapes.Relation rel)
        {
            int x = (line.X1 + line.X2) / 2; int y = (line.Y1 + line.Y2) / 2;
            string str = "";
            Brush brush = Brushes.Black;
            if (rel == Shapes.Relation.Equal)
            {
                str = "=";
                brush = Brushes.Red;
            }
            else if (rel == Shapes.Relation.Parallel)
            {
                str = "||";
                brush = Brushes.Purple;
            }

            if (rel != Shapes.Relation.Empty)
            {
                g.DrawString($"{str} {count}", new Font("Tahoma", 12, FontStyle.Bold), brush, x, y);
            }

        }


        static void Draw2(Graphics g, Shapes.Edge line)
        {
            int y1 = line.X1; int y2 = line.X2; int x1 = line.Y1; int x2 = line.Y2;
            bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
            if (steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = Math.Abs(y2 - y1);

            int error = dx - 2 * dy;
            int ystep = (y1 < y2) ? 1 : -1; ;
            int y = y1;

            int maxX = x2;

            for (int x = x1; x < maxX; x++)
            {
                if (steep)
                {
                    g.FillRectangle(Brushes.Black, x, y, 1, 1);
                }
                else
                {
                    g.FillRectangle(Brushes.Black, y, x, 1, 1);
                }

                error -= 2 * dy;
                if (error < 0)
                {
                    y += ystep;
                    error += 2 * dx;
                }
            }
        }

        // https://rosettacode.org/wiki/Bitmap/Bresenham%27s_line_algorithm#C.23
        static void Draw(Graphics graphics, Shapes.Edge line, Shapes.Relation rel, int count)
        {
            int x0 = line.X1; int x1 = line.X2; int y0 = line.Y1; int y1 = line.Y2;
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            string str = "";
            Brush brush = Brushes.Black;
            if (rel == Shapes.Relation.Equal)
            {
                str = "=";
                brush = Brushes.Red;
            }
            else if (rel == Shapes.Relation.Parallel)
            {
                str = "||";
                brush = Brushes.Purple;
            }

            if (rel != Shapes.Relation.Empty)
            {
                graphics.DrawString($"{str} {count}", new Font("Tahoma", 12, FontStyle.Bold), brush, (x0 + x1) / 2, (y0 + y1) / 2);
            }


            for (; ; )
            {
                graphics.FillRectangle(Brushes.Black, x0, y0, 1, 1);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }
    }
}
