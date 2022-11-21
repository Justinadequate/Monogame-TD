using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TDGame.Components;

namespace TDGame.Systems
{
    public class CollisionSystem
    {
        public List<Collider> _components = new List<Collider>();
        public List<Collider> _toRemove = new List<Collider>();

        public CollisionSystem()
        {
            EntityManager.Instance.OnComponentAdded += Instance_OnComponentAdded;
            EntityManager.Instance.OnComponentRemoved += Instance_OnComponentRemoved;
            EntityManager.Instance.OnEntityRemoved += Instance_OnEntityRemoved;
        }

        public void Update()
        {
            // var watch = new Stopwatch();
            // watch.Start();
            List<Collider> otherComponents = new List<Collider>();
            for (int i = 0; i < _components.Count; i++)
            {
                otherComponents.AddRange(_components);
                otherComponents.RemoveAt(i);
                for (int j = 0; j < otherComponents.Count; j++)
                {
                    if (_components[i].Bounds.Intersects(otherComponents[j].Bounds)
                        && _components[i].CollidingWith.FindIndex(e => e.Id == otherComponents[j].Entity.Id) == -1)
                    {
                        _components[i].CollidingWith.Add(otherComponents[j].Entity);
                        otherComponents[j].CollidingWith.Add(_components[i].Entity);
                    }

                    if (!_components[i].Bounds.Intersects(otherComponents[j].Bounds)
                        && _components[i].CollidingWith.FindIndex(e => e.Id == otherComponents[j].Entity.Id) != -1)
                    {
                        _components[i].CollidingWith.Remove(otherComponents[j].Entity);
                        otherComponents[j].CollidingWith.Remove(_components[i].Entity);
                    }
                }
                otherComponents.Clear();
            }
            // watch.Stop();
            // Debug.WriteLine("Collision System Update time: " + watch.ElapsedMilliseconds + " ms");
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
            if (component is Collider)
            {
                var collider = (Collider)component;
                if (!_components.Contains(collider))
                    _components.Add(collider);
            }
        }
        
        private void Instance_OnComponentRemoved(Entity entity, Component component)
        {
            if (component is Collider)
            {
                var collider = (Collider)component;
                if (_components.Contains(collider))
                    _toRemove.Add(collider);
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
}