using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TDGame.Components;
using TDGame.Components.Ui;
using TDGame.Models;

namespace TDGame.Systems;
public class UiSystem : System<UiItem>
{
    public UiSystem() : base() {}

    public override void Update(float deltaTime)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            var collider = Components[i].Entity.GetComponent<Collider>();
            var transform = Components[i].Entity.GetComponent<Transform>();

            if (Components[i].ItemType == UiItemType.Cursor)
            {
                var newPosition = Globals.MouseState.Position;
                transform.Destination = new Rectangle(newPosition, transform.Destination.Size);
                collider.Bounds = new Rectangle(newPosition, collider.Bounds.Size);
            }
            else if (Components[i].ItemType == UiItemType.Button)
            {
                if (collider.CollidingWith.Any(e => e.GetComponent<UiItem>().ItemType == UiItemType.Cursor) &&
                    Globals.MouseState.LeftButton == ButtonState.Pressed)
                    collider.Entity.GetComponent<Clickable>().OnClick.Invoke();
            }
        }
    }
    
    public override void Draw() {}
}