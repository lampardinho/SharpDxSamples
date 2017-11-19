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
            AddComponent<Transform>();
            transform = GetComponent<Transform>();
        }

        public void AddComponent<T>() where T : Component, new()
        {
            T c = new T();
            c.gameObject = this;
            components.Add(c);
        }

        public T GetComponent<T>() where T: Component
        {
            return components.Find(c => c.GetType() == typeof(T)) as T;
        }

        public void Update()
        {
            foreach (var c in components)
            {
                c.Update();
            }
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
