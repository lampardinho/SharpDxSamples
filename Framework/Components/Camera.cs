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
            get; private set;
        }

        public Matrix Projection = Matrix.Identity;
        public Matrix ViewMatrix = Matrix.Identity;

        public Matrix ViewProj = Matrix.Identity;

        public Camera()
        {
            Main = this;            
        }

        public override void Update()
        {
            base.Update();

            // Setup new projection matrix with correct aspect ratio
            //ViewMatrix = Matrix.LookAtLH(new Vector3(0, 0, -95), new Vector3(0, 0, 0), Vector3.UnitY);
            ViewMatrix = Matrix.Invert(gameObject.GetComponent<Transform>().ModelMatrix);
            Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, Application.Form.ClientSize.Width / (float)Application.Form.ClientSize.Height, 0.1f, 100.0f);

            ViewProj = Matrix.Multiply(ViewMatrix, Projection);
        }
    }
}
