using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Framework
{
    public class Input
    {
        public static Action<Keys> OnKeyDown;
        public static Action<Keys> OnKeyUp;
    }
}
