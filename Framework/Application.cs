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
        public Device device;

        RenderForm Form;
        List<GameObject> GameObjects;
        DeviceContext context;        
        SwapChain swapChain;
        Texture2D backBuffer;
        RenderTargetView renderView;

        public Application(string windowName, int width = 800, int height = 800)
        {
            GameObjects = new List<GameObject>();

            Form = new RenderForm(windowName)
            {
                ClientSize = new System.Drawing.Size(width, height)
            };
                       

            var swapDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(Form.ClientSize.Width, Form.ClientSize.Height,
                    new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = Form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.Debug,
                swapDesc, out device, out swapChain);

            backBuffer = swapChain.GetBackBuffer<Texture2D>(0);
            renderView = new RenderTargetView(device, backBuffer);

            context = device.ImmediateContext;            

            
            context.Rasterizer.SetViewport(new Viewport(0, 0, Form.ClientSize.Width, Form.ClientSize.Height, 0.0f, 1.0f));

            context.OutputMerger.SetTargets(renderView);

            // Create Constant Buffer
            //var contantBuffer = new Buffer(device, Utilities.SizeOf<Matrix>(), 
                //ResourceUsage.Default, BindFlags.ConstantBuffer, 
                //CpuAccessFlags.None, ResourceOptionFlags.None, 0);



            Form.KeyUp += (sender, args) =>
            {
                Input.OnKeyUp(args.KeyCode);
            };

            Form.KeyDown += (sender, args) =>
            {
                Input.OnKeyUp(args.KeyCode);
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
            go.device = device;
            return go;
        }
        
        public void Render()
        {
            //var viewProj = Matrix.Multiply(view, proj);

            // Clear views
            //context.ClearRenderTargetView(renderView, Color.Black);

            // Update WorldViewProj Matrix
            //var worldViewProj = Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f) * viewProj;
            //worldViewProj.Transpose();
            //context.UpdateSubresource(ref worldViewProj, contantBuffer);


            //===============================

            //context.ClearState();

            context.Rasterizer.SetViewport(new Viewport(0, 0, Form.ClientSize.Width, Form.ClientSize.Height, 0.0f, 1.0f));

            context.OutputMerger.SetTargets(renderView);
            context.ClearRenderTargetView(renderView, Color.Black);

            foreach(var go in GameObjects)
            {
                go.Render();
            }            

            swapChain.Present(0, PresentFlags.None);
        }

        public void ShutDown()
        {
            renderView.Dispose();
            backBuffer.Dispose();
            context.ClearState();
            context.Flush();
            device.Dispose();
            context.Dispose();
            swapChain.Dispose();
        }
    }
}
