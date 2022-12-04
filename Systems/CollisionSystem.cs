using System.Collections.Generic;
using TDGame.Components;

namespace TDGame.Systems;
public class CollisionSystem : System<Collider>
{
    public CollisionSystem() : base() {}

    public override void Update()
    {
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
    }

    public override void Draw() {}
}