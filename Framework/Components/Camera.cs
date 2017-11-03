using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using Device = SharpDX.Direct3D11.Device;

namespace Framework.Components
{
    public class Camera : Component
    {
        public static Camera Main 
        {
            get
            {
                return null;
            }
        }

        public Matrix Projection;

        public Camera(Device device) : base(device) { }
    }
}
