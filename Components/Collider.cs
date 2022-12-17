using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TDGame.Models;

namespace TDGame.Components;
public class Collider : Component
{
    public Rectangle Bounds { get; set; }
    public List<Entity> CollidingWith { get; set; }
    public CollisionLayer Layer { get; set; }
    public CollisionLayer Mask { get; set; }

    public Collider(Rectangle bounds, CollisionLayer layer, CollisionLayer mask, bool active = true) : base(active)
    {
        Bounds = bounds;
        Layer = layer;
        Mask = mask;
        CollidingWith = new List<Entity>();
    }
}
