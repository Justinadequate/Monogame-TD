using System.Linq;
using TDGame.Components;

namespace TDGame.Systems;
public class EnemySystem : System<Enemy>
{
    public EnemySystem() : base() {}

    public override void Update()
    {
        foreach (var component in _components)
        {
            var transform = component.Entity.GetComponent<Transform>();
            var collider = component.Entity.GetComponent<Collider>();

            if (!collider.CollidingWith.Any())
                continue;
            
            if (collider.CollidingWith.FirstOrDefault().TryGetComponent<Tile>(out var tile))
                transform.Position += tile.MoveDirection * component.MoveSpeed;

            collider.Bounds = transform.Destination;
            collider.Position = transform.Position;
        }
    }

    public override void Draw() {}
}