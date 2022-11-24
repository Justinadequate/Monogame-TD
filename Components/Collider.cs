using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TDGame.Components;
public class Collider : Component
{
    public Rectangle Bounds { get; set; }
    public Vector2 Position { get; set; }
    public List<Entity> CollidingWith { get; set; }

    public Collider(Rectangle bounds, Vector2 position, bool active = true) : base(active)
    {
        Bounds = bounds;
        Position = position;
        CollidingWith = new List<Entity>();
    }
}
