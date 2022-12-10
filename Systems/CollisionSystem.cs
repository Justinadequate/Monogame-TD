using System.Collections.Generic;
using TDGame.Components;

namespace TDGame.Systems;
public class CollisionSystem : System<Collider>
{
    public CollisionSystem() : base() {}

    public override void Update()
    {
        List<Collider> otherComponents = new List<Collider>();
        for (int i = 0; i < Components.Count; i++)
        {
            otherComponents.AddRange(Components);
            otherComponents.RemoveAt(i);
            for (int j = 0; j < otherComponents.Count; j++)
            {
                if (Components[i].Bounds.Intersects(otherComponents[j].Bounds)
                    && Components[i].Mask == otherComponents[j].Layer
                    && Components[i].CollidingWith.FindIndex(e => e.Id == otherComponents[j].Entity.Id) == -1)
                {
                    Components[i].CollidingWith.Add(otherComponents[j].Entity);
                    otherComponents[j].CollidingWith.Add(Components[i].Entity);
                }

                if (!Components[i].Bounds.Intersects(otherComponents[j].Bounds)
                    && Components[i].CollidingWith.FindIndex(e => e.Id == otherComponents[j].Entity.Id) != -1)
                {
                    Components[i].CollidingWith.Remove(otherComponents[j].Entity);
                    otherComponents[j].CollidingWith.Remove(Components[i].Entity);
                }
            }
            otherComponents.Clear();
        }
    }

    public override void Draw() {}
}