using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Framework
{
    public class BaseRenderer : Component
    {
        public Mesh mesh;
        public Material material;
        public Buffer VertexBuffer;

        private BufferDescription bufDescription;

        public BaseRenderer(Device device) : base(device)
        {
            material = new Material(device);
            mesh = new Mesh();

            bufDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default
            };

            VertexBuffer = Buffer.Create(device, mesh.vertices, bufDescription);

            var context = device.ImmediateContext;
            context.InputAssembler.InputLayout = material.layout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, 32, 0));

            context.VertexShader.Set(material.vertexShader);
            context.PixelShader.Set(material.pixelShader);

            context.Rasterizer.State = new RasterizerState(device, new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid
            });
        }

        public override void Render()
        {
            base.Render();

            var context = device.ImmediateContext;
            
            context.Draw(3, 0);
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
        }
    }
}
