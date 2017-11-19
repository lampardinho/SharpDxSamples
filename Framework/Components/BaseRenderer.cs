using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX;
using Framework.Components;

namespace Framework
{
    public class BaseRenderer : Component
    {
        public Mesh mesh;
        public Material material;
        public Buffer VertexBuffer;
        public Buffer constantBuffer;

        private BufferDescription bufDescription;

        VertexBufferBinding binding;

        public BaseRenderer()
        {
            material = new Material(RenderState.device);
            mesh = new Mesh();

            bufDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default,
            };

            VertexBuffer = Buffer.Create(RenderState.device, mesh.vertices, bufDescription);


            var context = RenderState.device.ImmediateContext;

            context.Rasterizer.State = new RasterizerState(RenderState.device, new RasterizerStateDescription
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid
            });


            binding = new VertexBufferBinding(VertexBuffer, 32, 0);

            // Create Constant Buffer
            constantBuffer = new Buffer(RenderState.device, Utilities.SizeOf<Matrix>(),
            ResourceUsage.Default, BindFlags.ConstantBuffer,
            CpuAccessFlags.None, ResourceOptionFlags.None, 0);
        }

        public override void Render()
        {
            base.Render();

            var context = RenderState.device.ImmediateContext;

            // Update WorldViewProj Matrix
            var time = 0;
            var worldViewProj = gameObject.transform.ModelMatrix * Camera.Main.ViewProj;
            context.UpdateSubresource(ref worldViewProj, constantBuffer);

            context.InputAssembler.InputLayout = material.layout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, binding);

            context.VertexShader.Set(material.vertexShader);
            context.VertexShader.SetConstantBuffer(0, constantBuffer);

            context.PixelShader.Set(material.pixelShader);

            
            context.Draw(3, 0);
        }

        public void Dispose()
        {
            VertexBuffer.Dispose();
        }
    }
}
