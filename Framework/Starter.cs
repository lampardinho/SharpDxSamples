using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    class Starter
    {
        static void Main()
        {
            Application app = new Application("App");

            GameObject go = app.Instantiate();
            go.AddComponent<BaseRenderer>();

            app.Run();
        }
    }
}
