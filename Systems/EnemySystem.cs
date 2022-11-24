using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TDGame.Components;

namespace TDGame.Systems;
public class EnemySystem
{
    private List<Enemy> _components = new List<Enemy>();
    private List<Enemy> _toRemove = new List<Enemy>();

    public EnemySystem()
    {
        EntityManager.Instance.OnComponentAdded += Instance_OnComponentAdded;
        EntityManager.Instance.OnComponentRemoved += Instance_OnComponentRemoved;
        EntityManager.Instance.OnEntityRemoved += Instance_OnEntityRemoved;
    }

    public void Update()
    {
        foreach (var component in _components)
        {
            var transform = component.Entity.GetComponent<Transform>();
            var collider = component.Entity.GetComponent<Collider>();

            if (!collider.CollidingWith.Any())
                continue;
            
            if (collider.CollidingWith.FirstOrDefault().TryGetComponent<Tile>(out var tile))
                transform.Position += tile.MoveDirection * component.MoveSpeed;

            collider.Bounds = transform.Destination;
            collider.Position = transform.Position;
        }
    }
    
    #region privates
    private void HandleRemove()
    {
        foreach (var item in _toRemove)
            _components.Remove(item);
        _toRemove.Clear();
    }

    private void Instance_OnComponentAdded(Entity entity, Component component)
    {
        if (component is Enemy)
        {
            var enemy = (Enemy)component;
            if (!_components.Contains(enemy))
                _components.Add(enemy);
        }
    }
    
    private void Instance_OnComponentRemoved(Entity entity, Component component)
    {
        if (component is Enemy)
        {
            var enemy = (Enemy)component;
            if (_components.Contains(enemy))
                _toRemove.Add(enemy);
        }
    }
    
    private void Instance_OnEntityRemoved(Entity entity)
    {
        var component = _components.FirstOrDefault(c => c.Entity.Id == entity.Id);
        if (component != null)
            _toRemove.Add(component);
    }
    #endregion
}