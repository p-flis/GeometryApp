using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometryApp.App
{
    enum Type { Down, Move, Up };
    struct Signal
    {
        public Type type;
        public MouseEventArgs mouseEvent;
    }
}
