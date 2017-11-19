using System.Collections.Generic;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using System.Windows.Forms;

namespace Framework
{
    public class Application
    {
        public static RenderForm Form;
        List<GameObject> GameObjects;
        RenderState renderState;

        public Application(string windowName, int width = 800, int height = 800)
        {
            GameObjects = new List<GameObject>();

            Form = new RenderForm(windowName)
            {
                ClientSize = new System.Drawing.Size(width, height)
            };


            renderState = new RenderState(Form);

            var input = new Input(this);
            

            Form.FormClosing += (sender, args) =>
            {
                ShutDown();
            };
        }

        public void Run()
        {
            RenderLoop.Run(Form, Render);
        }

        public GameObject Instantiate()
        {
            GameObject go = new GameObject();
            GameObjects.Add(go);
            return go;
        }
        
        private void Render()
        {
            renderState.ClearState();

            foreach (var go in GameObjects)
            {
                go.Update();
            }

            foreach (var go in GameObjects)
            {
                go.Render();
            }

            renderState.Present();
        }

        public void ShutDown()
        {
            renderState.Dispose();
        }
    }
}
