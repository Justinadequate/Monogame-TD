using System.Linq;
using TDGame.Components;

namespace TDGame.Systems;
public class EnemySystem : System<Enemy>
{
    public EnemySystem() : base() {}

    public override void Update()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var transform = _components[i].Entity.GetComponent<Transform>();
            var collider = _components[i].Entity.GetComponent<Collider>();

            if (!collider.CollidingWith.Any())
                continue;
            
            if (collider.CollidingWith.FirstOrDefault().TryGetComponent<Tile>(out var tile))
                transform.Position += tile.MoveDirection * _components[i].MoveSpeed;

            collider.Bounds = transform.Destination;
            collider.Position = transform.Position;
        }
    }

    public override void Draw() {}
}