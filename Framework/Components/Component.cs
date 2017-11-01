using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace Framework
{
    public class Component
    {
        public Device device;

        public Component(Device device)
        {
            this.device = device;
        }

        public virtual void Render()
        {

        }
    }
}
