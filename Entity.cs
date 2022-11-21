using System.Collections.Generic;
using System.Linq;

namespace TDGame
{
    public class Entity
    {
        private List<Component> _components { get; set; } = new List<Component>();
        public int Id { get; set; }
        public string Name { get; }
        public bool Active { get; set; }

        public Entity(string name, bool active = true)
        {
            Name = name;
            Active = active;
            EntityManager.Instance.AddEntity(this);
        }

        public IEnumerable<Component> GetComponents() => _components;
        public T GetComponent<T>() where T : Component => _components.OfType<T>().FirstOrDefault();

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = _components.OfType<T>().FirstOrDefault();

            if (component == null)
                return false;
            return true;
        }

        public void AddComponent(Component component)
        {
            if (!_components.Contains(component))
                _components.Add(component);
            EntityManager.Instance.ComponentAdded(this, component);
            component.Entity = this;
        }

        public void RemoveComponent(Component component)
        {
            if (_components.Contains(component))
                _components.Remove(component);
            EntityManager.Instance.ComponentRemoved(this, component);
        }
        
        public void Destroy()
        {
            EntityManager.Instance.RemoveEntity(this);
        }
    }
}