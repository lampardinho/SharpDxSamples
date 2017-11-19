using Framework;
using Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application("App");

            GameObject go = app.Instantiate();
            go.AddComponent<BaseRenderer>();

            GameObject cam = app.Instantiate();
            cam.AddComponent<Camera>();
            cam.AddComponent<CameraMoveBehaviour>();

            app.Run();
        }
    }
}
