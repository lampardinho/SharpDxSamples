using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace Framework
{
    public class GameObject
    {
        public Transform transform;
        public List<Component> components;

        public Device device;

        public GameObject()
        {
            components = new List<Component>();
        }

        public void AddComponent<T>() where T : Component
        {
            T c = (T)Activator.CreateInstance(typeof(T), device);
            components.Add(c);
            c.device = device;
        }

        public void Render()
        {
            foreach (var c in components)
            {
                c.Render();
            }
        }
    }
}
