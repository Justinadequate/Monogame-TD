using System.Collections.Generic;
using System.Linq;
using TDGame.Components;

namespace TDGame;
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

    public Entity AddComponents(params Component[] components)
    {
        for (int i = 0; i < components.Length; i++)
        {
            if (!_components.Contains(components[i]))
                _components.Add(components[i]);
            EntityManager.Instance.ComponentAdded(this, components[i]);
            components[i].Entity = this;
        }

        return this;
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