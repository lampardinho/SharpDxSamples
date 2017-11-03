using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using Device = SharpDX.Direct3D11.Device;

namespace Framework
{
    public class Transform : Component
    {
        public Vector3 Position;
        public Quaternion Rotaion;

        public Transform(Device device) : base(device) { }

        public Matrix ModelMatrix;
    }
}
