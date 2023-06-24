using Microsoft.Xna.Framework;
using TDGame.Components;
using TDGame.Util;

namespace TDGame.Systems;
public class EnemySystem : System<Enemy>
{
    public EnemySystem() : base(SystemTypes.Enemy) {}

    public override void Update(float deltaTime)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            var transform = Components[i].Entity.GetComponent<Transform>();
            var collider = Components[i].Entity.GetComponent<Collider>();

            if (collider.CollidingWith.Count < 1)
                continue;
            
            if (collider.CollidingWith[0].TryGetComponent<Tile>(out var tile))
            {
                var newPosition = transform.Destination.Location.ToVector2() + tile.MoveDirection * Components[i].MoveSpeed * deltaTime;
                transform.Destination = new Rectangle(newPosition.ToPoint(), transform.Destination.Size);
            }

            collider.Bounds = transform.Destination;
        }
    }

    public override void Draw() {}
}