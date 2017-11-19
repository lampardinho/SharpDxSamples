using Framework;
using Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;

namespace SolarSystem
{
    class CameraMoveBehaviour : Behaviour
    {
        public CameraMoveBehaviour()
        {
        }

        public override void Update()
        {
            base.Update();

            if (Input.Instance.IsKeyDown(Keys.W))
            {
                gameObject.transform.Position += new Vector3(0, 0, 1);
                gameObject.transform.ModelMatrix.TranslationVector = gameObject.transform.Position;
            }

            if (Input.Instance.IsKeyDown(Keys.S))
            {
                gameObject.transform.Position += new Vector3(0, 0, 1);
                gameObject.transform.ModelMatrix.TranslationVector = gameObject.transform.Position;
            }

            gameObject.transform.ModelMatrix = Matrix.RotationYawPitchRoll(Input.Instance.MouseOffset.X, Input.Instance.MouseOffset.Y, 0);
        }
    }
}
