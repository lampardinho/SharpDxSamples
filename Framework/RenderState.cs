using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using Framework.Components;

namespace Framework
{
    class RenderState
    {
        RenderForm Form;
        public static Device device;
        static DeviceContext context;
        static SwapChain swapChain;
        static Texture2D backBuffer;
        static RenderTargetView renderView;       

        public RenderState(RenderForm form)
        {
            Form = form;

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
        }

        public void ClearState()
        {
            //context.ClearState();
            context.ClearRenderTargetView(renderView, Color.Black);

            

            
        }

        public void Present()
        {
            swapChain.Present(0, PresentFlags.None);
        }

        public void Dispose()
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
