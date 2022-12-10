using TDGame.Components;

namespace TDGame.Systems;
public class EnemySystem : System<Enemy>
{
    public EnemySystem() : base() {}

    public override void Update()
    {
        for (int i = 0; i < Components.Count; i++)
        {
            var transform = Components[i].Entity.GetComponent<Transform>();
            var collider = Components[i].Entity.GetComponent<Collider>();

            if (collider.CollidingWith.Count < 1)
                continue;
            
            if (collider.CollidingWith[0].TryGetComponent<Tile>(out var tile))
                transform.Position += tile.MoveDirection * Components[i].MoveSpeed;

            collider.Bounds = transform.Destination;
            collider.Position = transform.Position;
        }
    }

    public override void Draw() {}
}