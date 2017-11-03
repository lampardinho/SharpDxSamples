using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace Framework
{
    public class Material
    {
        public VertexShader vertexShader;
        public PixelShader pixelShader;
        public string VertexShaderPath;
        public string PixelShaderPath;
        public InputLayout layout;

        private CompilationResult vertexShaderByteCode;
        private CompilationResult pixelShaderByteCode;

        public Material(Device device)
        {
            vertexShaderByteCode = ShaderBytecode.CompileFromFile(@".\MiniTri.fx", "VSMain", "vs_5_0");
            vertexShader = new VertexShader(device, vertexShaderByteCode);

            pixelShaderByteCode = ShaderBytecode.CompileFromFile(@".\MiniTri.fx", "PSMain", "ps_5_0");
            pixelShader = new PixelShader(device, pixelShaderByteCode);

            layout = new InputLayout(
                device,
                ShaderSignature.GetInputSignature(vertexShaderByteCode),
                new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                });
        }

        public void Render()
        {
            
        }

        public void Dispose()
        {
            vertexShaderByteCode.Dispose();
            vertexShader.Dispose();
            pixelShaderByteCode.Dispose();
            pixelShader.Dispose();
            layout.Dispose();
        }
    }
}
