using System.Collections.Generic;
using System.Linq;
using TDGame.Components;

namespace TDGame.Systems;
public abstract class System<T> : ISystem where T : Component
{
    public List<T> _components { get; }
    public List<T> _toRemove { get; }

    public System()
    {
        EntityManager.Instance.OnComponentAdded += Instance_OnComponentAdded;
        EntityManager.Instance.OnComponentRemoved += Instance_OnComponentRemoved;
        EntityManager.Instance.OnEntityRemoved += Instance_OnEntityRemoved;

        _components = new List<T>();
        _toRemove = new List<T>();
    }

    public abstract void Update();
    public abstract void Draw();
    
    private void HandleRemove()
    {
        foreach (var item in _toRemove)
            _components.Remove(item);
        _toRemove.Clear();
    }

    private void Instance_OnComponentAdded(Entity entity, Component addedComponent)
    {
        if (addedComponent is T)
        {
            var component = (T)addedComponent;
            if (!_components.Contains(component))
                _components.Add(component);
        }
    }
    
    private void Instance_OnComponentRemoved(Entity entity, Component removedComponent)
    {
        if (removedComponent is T)
        {
            var component = (T)removedComponent;
            if (_components.Contains(component))
                _toRemove.Add(component);
        }
    }
    
    private void Instance_OnEntityRemoved(Entity entity)
    {
        var component = _components.FirstOrDefault(c => c.Entity.Id == entity.Id);
        if (component != null)
            _toRemove.Add(component);
    }
}