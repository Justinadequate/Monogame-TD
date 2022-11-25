using System;
using System.Collections.Generic;

namespace TDGame;
public class EntityManager
{
    private List<Entity> Entities = new List<Entity>();
    public static EntityManager Instance { get; set; }
    public event Action<Entity, Component> OnComponentAdded;
    public event Action<Entity, Component> OnComponentRemoved;
    public event Action<Entity> OnEntityAdded;
    public event Action<Entity> OnEntityRemoved;

    public EntityManager()
    {
        Instance = this;
    }

    public List<Entity> GetEntities() => Entities;

    public void AddEntity(Entity entity)
    {
        if (!Entities.Contains(entity))
        {
            Entities.Add(entity);
            entity.Id = Entities.Count - 1;
            OnEntityAdded?.Invoke(entity);
        }
    }
    
    public void RemoveEntity(Entity entity)
    {
        if (Entities.Contains(entity))
        {
            Entities.Remove(entity);
            ReIndex();
            OnEntityRemoved?.Invoke(entity);
        }
    }

    public void ComponentAdded(Entity entity, Component component)
    {
        OnComponentAdded?.Invoke(entity, component);
    }

    public void ComponentRemoved(Entity entity, Component component)
    {
        OnComponentRemoved?.Invoke(entity, component);
    }

    private void ReIndex()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].Id = i;

            foreach (var component in Entities[i].GetComponents())
            {
                component.Entity.Id = i;
            }
        }
    }
}