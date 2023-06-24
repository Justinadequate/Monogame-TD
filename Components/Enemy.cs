using Microsoft.Xna.Framework;
using TDGame.Util;

namespace TDGame.Components;
public class Enemy : Component
{
    public float Health { get; set; }
    public float MoveSpeed { get; set; }
    public Vector2 Destination { get; set; }

    public Enemy(int health, float moveSpeed, bool active = true) : base(ComponentTypes.Enemy, active)
    {
        Health = health;
        MoveSpeed = moveSpeed;
    }
}