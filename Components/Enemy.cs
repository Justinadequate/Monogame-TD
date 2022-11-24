using Microsoft.Xna.Framework;

namespace TDGame.Components;
public class Enemy : Component
{
    public float Health { get; set; }
    public float MoveSpeed { get; set; }
    public Vector2 Destination { get; set; }

    public Enemy(int health, float moveSpeed, bool active = true) : base(active)
    {
        Health = health;
        MoveSpeed = moveSpeed;
    }
}