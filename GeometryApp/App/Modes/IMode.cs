using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryApp.App.Modes
{
    interface IMode
    {
        void HandleClick(Signal signal);
        bool Finalized { get; }
    }

    enum Mode {RemovePolygon, MovePoint, MovePolygon, NewPolygon, EqualRelation, ParallelRelation, DivideEdge, RemoveVertex, RemoveRelation}
}
