using Microsoft.Xna.Framework;

namespace TDGame.Components;
public class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public Rectangle Destination { get; set; }
    public float Rotation { get; set; }
    // TODO: origin here?

    public Transform(Vector2 position, bool active = true) : base(active)
    {
        Position = position;
        Scale = Vector2.One;
        Rotation = 0f;
    }
}