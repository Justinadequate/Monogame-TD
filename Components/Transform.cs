using Microsoft.Xna.Framework;
using TDGame.Util;

namespace TDGame.Components;
public class Transform : Component
{
    public Vector2 Scale { get; set; }
    public Rectangle Destination { get; set; }
    public float Rotation { get; set; }
    // TODO: origin here?

    public Transform(Point position, Point size, bool active = true) : base(ComponentTypes.Transform, active)
    {
        Scale = Vector2.One;
        Rotation = 0f;
        Destination = new Rectangle(position, size);
    }

    public Transform(Transform transform, bool active = true) : base(ComponentTypes.Transform, active)
    {
        Scale = transform.Scale;
        Destination = transform.Destination;
        Rotation = transform.Rotation;
    }
}