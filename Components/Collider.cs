using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TDGame.Models;

namespace TDGame.Components;
public class Collider : Component
{
    public Rectangle Bounds { get; set; }
    public Vector2 Position { get; set; }
    public List<Entity> CollidingWith { get; set; }
    public CollisionLayer Layer { get; set; }
    public CollisionLayer Mask { get; set; }

    public Collider(Rectangle bounds, Vector2 position, CollisionLayer layer, CollisionLayer mask, bool active = true) : base(active)
    {
        Bounds = bounds;
        Position = position;
        Layer = layer;
        Mask = mask;
        CollidingWith = new List<Entity>();
    }
}
