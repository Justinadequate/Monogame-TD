using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Components;

namespace TDGame.Systems
{
    public class DrawingSystem
    {
        private SpriteBatch _spriteBatch;
        private List<Rendering> _components = new List<Rendering>();
        private List<Rendering> _toRemove = new List<Rendering>();

        public DrawingSystem(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            EntityManager.Instance.OnComponentAdded += Instance_OnComponentAdded;
            EntityManager.Instance.OnComponentRemoved += Instance_OnComponentRemoved;
            EntityManager.Instance.OnEntityRemoved += Instance_OnEntityRemoved;
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                var transform = component.Entity.GetComponent<Transform>();
                transform.Destination = new Rectangle(
                    (int)transform.Position.X,
                    (int)transform.Position.Y,
                    (int)Math.Floor(component.Source.Width * transform.Scale.X),
                    (int)Math.Floor(component.Source.Height * transform.Scale.Y)
                );
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                var transform = _components[i].Entity.GetComponent<Transform>();
                _spriteBatch.Draw(
                    texture: _components[i].Texture,
                    destinationRectangle: transform.Destination,
                    sourceRectangle: _components[i].Source,
                    color: _components[i].DrawColor,
                    rotation: transform.Rotation,
                    origin: new Vector2(
                        transform.Destination.Width/2,
                        transform.Destination.Height/2
                    ),
                    effects: SpriteEffects.None,
                    layerDepth: _components[i].Layer
                );
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
            if (component is Rendering)
            {
                var renderer = (Rendering)component;
                if (!_components.Contains(renderer))
                    _components.Add(renderer);
            }
        }
        
        private void Instance_OnComponentRemoved(Entity entity, Component component)
        {
            if (component is Rendering)
            {
                var renderer = (Rendering)component;
                if (_components.Contains(renderer))
                    _toRemove.Add(renderer);
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