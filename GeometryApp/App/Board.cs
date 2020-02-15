using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GeometryApp.App.Modes;
using System.Numerics;

namespace GeometryApp.App
{
    class Board
    {
        public List<Shapes.Polygon> polygons = new List<Shapes.Polygon>();

        Label _l;

        Random random = new Random();

        int cnt = 0;

        public void setLabel(Label l)
        {
            _l = l;
        }

        public void DefaultInit()
        {
            var poly = new Shapes.Polygon();
            poly.points.Add(new Shapes.Point { R = 5, X = 100, Y = 100 });
            poly.points.Add(new Shapes.Point { R = 5, X = 200, Y = 200 });
            poly.points.Add(new Shapes.Point { R = 5, X = 300, Y = 250 });
            poly.points.Add(new Shapes.Point { R = 5, X = 400, Y = 250 });
            poly.points.Add(new Shapes.Point { R = 5, X = 500, Y = 300 });
            poly.points.Add(new Shapes.Point { R = 5, X = 600, Y = 180 });
            poly.points.Add(new Shapes.Point { R = 5, X = 550, Y = 150 });
            poly.points.Add(new Shapes.Point { R = 5, X = 450, Y = 90 });
            poly.points.Add(new Shapes.Point { R = 5, X = 350, Y = 90 });
            poly.points.Add(new Shapes.Point { R = 5, X = 250, Y = 40 });

            SetRelation(poly, Shapes.Relation.Parallel, 5, 7);
            SetRelation(poly, Shapes.Relation.Parallel, 0, 4);

            SetRelation(poly, Shapes.Relation.Equal, 6, 2);
            SetRelation(poly, Shapes.Relation.Equal, 1, 3);

            polygons.Add(poly);

        }

        public Board()
        {
            DefaultInit();
        }

        public void RemovePolygon(Shapes.Polygon polygon)
        {
            polygons.Remove(polygon);
        }

        public void RemoveVertex(Shapes.Polygon polygon, int index)
        {
            var prev = polygon.PrevInd(index);
            RmRel(polygon, prev);
            RmRel(polygon, index);
            polygon.points.RemoveAt(index);
            foreach (var point in polygon.points)
            {
                if (point.relation != Shapes.Relation.Empty && point.relatedIndex >= index)
                {
                    point.relatedIndex = polygon.PrevInd(point.relatedIndex);
                }
            }
        }

        public void DivideEdge(Shapes.Polygon polygon, int index)
        {
            var p = polygon.points[index];

            RmRel(polygon, index);

            var pN = polygon.Next(index);
            int x = (p.X + pN.X) / 2; int y = (p.Y + pN.Y) / 2;

            var newPoint = new Shapes.Point { X = x, Y=y, relation=Shapes.Relation.Empty, R=5};

            var tmpPos = polygon.NextInd(index);
            if (tmpPos == 0)
            {
                polygon.points.Add(newPoint);
            }
            else
            {
                polygon.points.Insert(tmpPos, newPoint);

                foreach (var point in polygon.points)
                {
                    if (point.relation != Shapes.Relation.Empty && point.relatedIndex >= tmpPos)
                    {
                        point.relatedIndex = polygon.NextInd(point.relatedIndex);
                    }
                }

            }
        }

        public void RmRel(Shapes.Polygon polygon, int ind)
        {
            
            var p = polygon.Curr(ind);
            p.number = 0;
            if (p.relation == Shapes.Relation.Empty) return;

            var pR = polygon.points[p.relatedIndex];

            pR.relatedIndex = 0;
            pR.relation = Shapes.Relation.Empty;
            pR.number = 0;

            p.relatedIndex = 0;
            p.relation = Shapes.Relation.Empty;
            p.number = 0;
        }

        public void SetRelation(Shapes.Polygon polygon, Shapes.Relation relation, int index1, int index2)
        {
            polygon.points[index1].relation = relation; polygon.points[index1].relatedIndex = index2;
            polygon.points[index2].relation = relation; polygon.points[index2].relatedIndex = index1;

            int[] t = new int[10];
            foreach(var p in polygon.points)
            {
                if(p.relation == relation)
                    t[p.number] = 1;
            }
            int i = 0;
            for(i = 0; i < 10; i++)
            {
                if (t[i] == 0) break;
            }
            polygon.points[index2].number = i; polygon.points[index1].number = i;

            RelaxationEdge(polygon, index1, index2);
        }

        public void RelaxationEdge(Shapes.Polygon polygon, int p1, int p2)
        {
            //actual
            int[] set = new int[polygon.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(p1);
            queue.Enqueue(p2);
            int pointIndex;
            while (queue.Count > 0)
            {
                pointIndex = queue.Dequeue();
                RepairVertex(polygon, pointIndex, set, queue);
                RepairVertex(polygon, (pointIndex - 1 + polygon.Count) % polygon.Count, set, queue);
            }
        }
        public bool RelaxationVertex(Shapes.Polygon polygon, int pointIndex, int dx, int dy)
        {
            //actual
            //SpecialCases(polygon, pointIndex, dx, dy);
            int[] set = new int[polygon.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(pointIndex);
            
            while (queue.Count > 0)
            {
                pointIndex = queue.Dequeue();
                RepairVertex(polygon, pointIndex, set, queue);
                RepairVertex(polygon, (pointIndex - 1 + polygon.Count) % polygon.Count, set, queue);
            }
            foreach(var ind in set)
            {
                if(ind > 2)
                {
                    return false;
                }
            }
            return true;

        }

        //public void SpecialCases(Shapes.Polygon polygon, int index, int dx, int dy)
        //{
        //    var p = polygon.Curr(index);
        //    var pp = polygon.Prev(index);
        //    var pn = polygon.Next(index);
        //    if (p.relation != Shapes.Relation.Empty && pp.relatedIndex == polygon.points[pn.relatedIndex].relatedIndex)
        //    {
        //        pp.X += dx;
        //        pn.X += dx;
        //        p.X += dx;

        //        pp.Y += dy;
        //        pn.Y += dy;
        //        p.Y += dy;
        //    }
        //}

        public void RepairVertex(Shapes.Polygon polygon, int index, int[] set, Queue<int> queue)
        {
            if (set[index] > 2) return;
            set[index]++;
            var p1 = polygon.points[index];
            var pp = polygon.Prev(p1.relatedIndex);
            var pn = polygon.Next(p1.relatedIndex);
            var pc = polygon.Curr(p1.relatedIndex);

            int tmpind;

            switch (p1.relation)
            {
                case Shapes.Relation.Parallel:
                    if (pp.relation == Shapes.Relation.Empty)
                    {
                        RotateEdgeFull(polygon, index, 1);
                        break;
                    }

                    if (pn.relation == Shapes.Relation.Empty)
                    {
                        RotateEdgeFull(polygon, index, 0);
                        break;
                    }
                    if(pp.relation == Shapes.Relation.Parallel)
                    {
                        RotateEdgeFull(polygon, index, 1);
                        queue.Enqueue((p1.relatedIndex - 1 +polygon.Count) % polygon.Count);
                        break;
                    }
                    if (pn.relation == Shapes.Relation.Parallel)
                    {
                        RotateEdgeFull(polygon, index, 0);
                        queue.Enqueue((p1.relatedIndex + 1 + polygon.Count) % polygon.Count);
                        break;
                    }
               
                    if (pp.relation == Shapes.Relation.Equal)
                    {
                        var dpos = RotateEdgeFull(polygon, index, 1);
                        //pp.X += dpos.dx/2; pp.Y += dpos.dy/2;
                        ResizeEdge(polygon, pp.relatedIndex, 0);
                        RotateEdgeFull(polygon, index, 1);
                        queue.Enqueue((p1.relatedIndex - 1 + polygon.Count) % polygon.Count);
                        queue.Enqueue((p1.relatedIndex - 2 + polygon.Count) % polygon.Count);
                        break;
                    }
                    if (pn.relation == Shapes.Relation.Equal)
                    {
                        var dpos = RotateEdgeFull(polygon, index, 0);
                        ResizeEdge(polygon, pn.relatedIndex, 1);
                        RotateEdgeFull(polygon, index, 0);
                        queue.Enqueue((p1.relatedIndex + 1 + polygon.Count) % polygon.Count);
                        queue.Enqueue((p1.relatedIndex + 2 + polygon.Count) % polygon.Count);
                        break;
                    }
                    break;

                case Shapes.Relation.Equal:
                    if (pp.relation == Shapes.Relation.Empty)
                    {
                        ResizeEdge(polygon, index, 1);
                        break;
                    }

                    if (pn.relation == Shapes.Relation.Empty)
                    {
                        ResizeEdge(polygon, index, 0);
                        break;
                    }


                    if (pp.relation == Shapes.Relation.Parallel)
                    {
                        int x = pc.X; int y = pc.Y;
                        ResizeEdge(polygon, index, 1);
                        RotateEdgeFull(polygon, pp.relatedIndex, 0);
                        ResizeEdge(polygon, index, 1);
                        int dx = pc.X - x; int dy = pc.Y - y;
                        //pp.X += dx; pp.Y += dy;
                        queue.Enqueue((p1.relatedIndex - 1 + polygon.Count) % polygon.Count);
                        queue.Enqueue((p1.relatedIndex - 2 + polygon.Count) % polygon.Count);
                        break;
                    }
                    if (pn.relation == Shapes.Relation.Parallel)
                    {
                        int x = pc.X; int y = pc.Y;
                        ResizeEdge(polygon, index, 0);
                        RotateEdgeFull(polygon, pn.relatedIndex, 1);
                        ResizeEdge(polygon, index, 0);
                        int dx = pc.X - x; int dy = pc.Y - y;
                        //pn.X += dx; pn.Y += dy;
                        queue.Enqueue((p1.relatedIndex + 1 + polygon.Count) % polygon.Count);
                        queue.Enqueue((p1.relatedIndex + 2 + polygon.Count) % polygon.Count);
                        break;
                    }

                    //if (pp.relatedIndex == polygon.points[pn.relatedIndex].relatedIndex)
                    //{
                    //    ResizeEdge(polygon, pp.relatedIndex, 1);
                    //    MessageBox.Show("ddd");
                    //}

                    if (pn.relation == Shapes.Relation.Equal && p1.relatedIndex == polygon.NextInd(index))
                    {
                        //MessageBox.Show("ddd");
                        ResizeEdge(polygon, index, 0);
                        queue.Enqueue((p1.relatedIndex + 1 + polygon.Count) % polygon.Count);
                        break;
                    }
                    //todo
                    if (pp.relation == Shapes.Relation.Equal)
                    {
                        tmpind = (p1.relatedIndex - 1 + polygon.Count) % polygon.Count;
                        ResizeEdge(polygon, index, 1);
                        queue.Enqueue(tmpind);
                        break;
                    }
                    //todo
                    if (pn.relation == Shapes.Relation.Equal)
                    {
                        ResizeEdge(polygon, index, 0);
                        queue.Enqueue((p1.relatedIndex + 1 + polygon.Count) % polygon.Count);
                        break;
                    }
                    break;
            }
        }

        public void Relaxation(Shapes.Polygon polygon)
        {
            var queueEqual = new Queue<int>();
            var queueParallel = new Queue<int>();
            int i = 0;

            foreach (var p in polygon.points)
            {
                if(p.relation == Shapes.Relation.Parallel)
                {
                    //??
                    if(!queueParallel.Contains(p.relatedIndex) && !queueParallel.Contains(i))
                        queueParallel.Enqueue(i);
                    //fancy algo
                }
                if (p.relation == Shapes.Relation.Equal)
                {
                    //??
                    if (!queueEqual.Contains(p.relatedIndex) && !queueEqual.Contains(i))
                        queueEqual.Enqueue(i);
                    //fancy algo
                }
                i++;
            }
           

            while(queueEqual.Count > 0)
            {

                int i1 = queueEqual.Dequeue();
                int i2 = polygon.points[i1].relatedIndex;

                if (polygon.points[i1].relation == Shapes.Relation.Equal)
                {
                    int d1 = Length(polygon, i1);
                    int d2 = Length(polygon, i2);

                    float coef = (d1 + d2) / (float)d2;
                    if (d2 == 0 || d1 == 0) continue;
                    ResizeEdge(polygon, i2, ((d1 + d2) / 2) / (float)d2);
                    ResizeEdge(polygon, i1, d2 / ((float)(d1 + d2) / 2));
                }

            }
            while (queueParallel.Count > 0)
            {
                int i1 = queueParallel.Dequeue();
                if (polygon.points[i1].relation == Shapes.Relation.Parallel)
                {
                    int relind = polygon.points[i1].relatedIndex;

                    var p1 = polygon.points[i1];
                    var p2 = polygon.points[(i1 + 1) % polygon.Count];

                    var p3 = polygon.points[(relind + 0) % polygon.Count];
                    var p4 = polygon.points[(relind + 1) % polygon.Count];

                    double angle = CalcAngle(p1, p2, p3, p4);

                    RotateEdge(p3, p4, angle / 2);
                    RotateEdge(p2, p1, -angle / 2);
                }
            }
        }

        void ResizeEdge(Shapes.Polygon polygon, int index, float coef)
        {
            var p1 = polygon.points[index];
            var p2 = polygon.points[(index + 1) % polygon.Count];

            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;

            double tdx = (dx * coef);
            double tdy = (dy * coef);
            tdx = dx - tdx;
            tdy = dy - tdy;

            dx = (int)Math.Round(tdx / 2);
            dy = (int)Math.Round(tdy / 2);

            int x1 = p1.X; int y1 = p1.Y; int x2 = p2.X; int y2 = p2.Y;

            p2.X = x2 - dx;
            p2.Y = y2 - dy;

            dx = (int)Math.Round((tdx+1) / 2);
            dy = (int)Math.Round((tdy+1) / 2);

            p1.X = x1 + dx;
            p1.Y = y1 + dy;
        }

        int Length(Shapes.Polygon polygon, int index)
        {
            int i1 = index;
            int i2 = (i1 + 1) % polygon.Count;
            var p1 = polygon.points[i1]; var p2 = polygon.points[i2];
            int xx = p1.X - p2.X; int yy = p1.Y - p2.Y;
            xx *= xx; yy *= yy;
            int d = (int)Math.Round(Math.Sqrt(xx + yy));
            return d;
        }

        public void Relaxation()
        {
            foreach(var poly in polygons)
            {
                for(int i = 0; i < 5; i++)
                Relaxation(poly);
            }
        }

        void RotateEdge(Shapes.Point p1, Shapes.Point p2, double angle)
        {
            if (Math.Abs(angle) < 0.005) return;
            double cosangle = Math.Cos(angle);
            double sinangle = Math.Sin(angle);

            int dx = p2.X - p1.X; int dy = p2.Y - p1.Y;

            p2.X = (int)Math.Round((cosangle * dx - sinangle * dy) + p1.X);
            p2.Y = (int)Math.Round((sinangle * dx + cosangle * dy) + p1.Y);
        }

        //p1 (index) --- p2; p3 --- p4; way == 0 -> p3 rotated; way != 0 -> p4 rotated; 
        (int dx, int dy) RotateEdgeFull(Shapes.Polygon polygon, int index, int way)
        {
            index = (index + polygon.Count) % polygon.Count;
            var p1 = polygon.points[index];
            var p2 = polygon.Next(index);
            var p3 = polygon.points[p1.relatedIndex];
            var p4 = polygon.Next(p1.relatedIndex);
            double ang = CalcAngle(p1, p2, p3, p4);
            if (way == 0)
            {
                int x = p4.X;
                int y = p4.Y;
                RotateEdge(p3, p4, ang);
                return (p4.X - x, p4.Y - y);
            }
            else
            {
                int x = p3.X;
                int y = p3.Y;
                RotateEdge(p4, p3, ang);
                return (p3.X - x, p3.Y - y);
            }
        }

        void ResizeEdge(Shapes.Polygon polygon, int index, int way)
        {
            int d1 = Length(polygon, index);
            int d2 = Length(polygon, polygon.Curr(index).relatedIndex);

            var p3 = polygon.Curr(polygon.Curr(index).relatedIndex);
            var p4 = polygon.Next(polygon.Curr(index).relatedIndex);

            if (d2 == 0) return;
            double coef = (d1 / (double)d2);

            var p1 = polygon.Curr(index) ; var p2 = polygon.Next(index);

            if (way == 0)
            {
                ResizeEdge(p3, p4, coef);
            }
            else
            {
                ResizeEdge(p4, p3, coef);
            }
        }

        void ResizeEdge(Shapes.Point p1, Shapes.Point p2, double coef)
        {
            int dx = p2.X - p1.X; int dy = p2.Y - p1.Y;
            
            dx = (int)Math.Round(coef * dx);
            dy = (int)Math.Round(coef * dy);

            p2.X = p1.X + dx;

            p2.Y = p1.Y + dy;
        }

        double CalcAngle(Shapes.Point p11, Shapes.Point p12, Shapes.Point p21, Shapes.Point p22)
        {
            int dx1 = p12.X - p11.X;
            int dy1 = p12.Y - p11.Y;

            int dx2 = p22.X - p21.X;
            int dy2 = p22.Y - p21.Y;


            double sin = dx1 * dy2 - dx2 * dy1;
            double cos = dx1 * dx2 + dy1 * dy2;

            double angle = -Math.Atan2(sin, cos);
            if(angle < -Math.PI/2)
            {
                angle = -Math.PI - angle;
                angle *= -1;
            }
            if(angle > Math.PI/2)
            {
                angle = Math.PI - angle;
                angle *= -1;
            }

            return angle;
        }

        public (Shapes.Polygon polygon, int pointIndex) FindEdge(int x, int y)
        {
            foreach (var poly in polygons)
            {
                for(int i = 0; i < poly.Count; i++)
                {
                    Shapes.Point p1 = poly.points[i]; Shapes.Point p2 = poly.points[(i + 1) % poly.Count];
                    if (nearEdge(x, y, p1.X, p1.Y, p2.X, p2.Y))
                    {
                        return (poly, i);
                    }
                }
            }
            return (null, -1);
        }

        public (Shapes.Polygon polygon, int pointIndex) FindPoint(int x, int y)
        {
            foreach(var poly in polygons)
            {
                int i = 0;
                foreach(var p in poly.points)
                {
                    if (insidePoint(p, x, y))
                    {
                        return (poly, i);
                    }
                    i++;
                }
            }
            return (null, -1);
        }
        
        public void MoveEdge(Shapes.Polygon polygon, int pointIndex, int dx, int dy)
        {
            int i1 = pointIndex;
            int i2 = (pointIndex + 1) % polygon.Count;

            polygon.points[i1].X += dx; polygon.points[i1].Y += dy;
            polygon.points[i2].X += dx; polygon.points[i2].Y += dy;

            RelaxationEdge(polygon, i1, i2);
        }

        public void MovePoint(Shapes.Polygon polygon, int pointIndex, int dx, int dy)
        {
            var p1 = polygon.points[pointIndex];
            polygon.points[pointIndex].X += dx;
            polygon.points[pointIndex].Y += dy;

            RelaxationVertex(polygon, pointIndex, dx, dy);
            //Relaxation(polygon);
        }

        public void MovePolygon(Shapes.Polygon polygon, int dx, int dy)
        {
            foreach(var p in polygon.points)
            {
                p.X += dx;
                p.Y += dy;
            }
        }

        bool insidePoint(Shapes.Point p, int x, int y)
        {
            int tmpx = p.X - x;
            int tmpy = p.Y - y;
            tmpx *= tmpx;
            tmpy *= tmpy;
            return p.R * p.R >= tmpx + tmpy;
        }

        bool nearEdge(int x, int y, int x1, int y1, int x2, int y2)
        {
            int distance = distanceSquare(x, y, x1, y1, x2, y2);
            if (_l != null) _l.Text = $"distance: {distance} : x, y = {x}, {y} : x1, y1 = {x1}, {y1} : x2, y2 = {x2}, {y2}";
            return distance < 5;
        }

        // https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment
        int distanceSquare(int x3, int y3, int x1, int y1, int x2, int y2)
        {
            int px = x2 - x1;
            int py = y2 - y1;

            float norm = px * px + py * py;

            float u = ((x3 - x1) * px + (y3 - y1) * py) / norm;

            if (u > 1)
                u = 1;
            else if (u < 0)
                u = 0;

            float x = x1 + u * px;
            float y = y1 + u * py;

            float dx = x - x3;
            float dy = y - y3;

            return (int)Math.Sqrt(dx * dx + dy * dy);

        }
    }
}
