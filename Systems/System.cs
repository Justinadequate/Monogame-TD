using System.Collections.Generic;
using System.Linq;
using TDGame.Components;

namespace TDGame.Systems;
public abstract class System<T> : ISystem where T : Component
{
    public List<T> Components { get; }
    public List<T> ToRemove { get; }

    public System()
    {
        EntityManager.Instance.OnComponentAdded += Instance_OnComponentAdded;
        EntityManager.Instance.OnComponentRemoved += Instance_OnComponentRemoved;
        EntityManager.Instance.OnEntityRemoved += Instance_OnEntityRemoved;

        Components = new List<T>();
        ToRemove = new List<T>();
    }

    public abstract void Update(float deltaTime);
    public abstract void Draw();
    
    private void HandleRemove()
    {
        for (int i = 0; i < ToRemove.Count; i++)
            Components.Remove(ToRemove[i]);
        ToRemove.Clear();
    }

    private void Instance_OnComponentAdded(Entity entity, Component addedComponent)
    {
        if (addedComponent is T)
        {
            var component = (T)addedComponent;
            if (!Components.Contains(component))
                Components.Add(component);
        }
    }
    
    private void Instance_OnComponentRemoved(Entity entity, Component removedComponent)
    {
        if (removedComponent is T)
        {
            var component = (T)removedComponent;
            if (Components.Contains(component))
                ToRemove.Add(component);
        }
    }
    
    private void Instance_OnEntityRemoved(Entity entity)
    {
        var component = Components.FirstOrDefault(c => c.Entity.Id == entity.Id);
        if (component != null)
            ToRemove.Add(component);
    }
}