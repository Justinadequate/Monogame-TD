using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TDGame.Components;
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
            // TODO: onclick/onhover/onwhatever
            // UI might need its own entity structure to handle events
            // try desperately to remember your frontend experience

            if (Components[i].ItemType == UiItemType.Cursor)
            {
                var newPosition = Globals.MouseState.Position;
                transform.Destination = new Rectangle(newPosition, transform.Destination.Size);
                collider.Bounds = new Rectangle(newPosition, collider.Bounds.Size);
                if (Globals.MouseState.LeftButton == ButtonState.Pressed)
                    Debug.WriteLine(transform.Destination.Location);
            }
            else if (Components[i].ItemType == UiItemType.Button)
            {
                if (collider.CollidingWith.Any(e => e.GetComponent<UiItem>().ItemType == UiItemType.Cursor))
                    Debug.WriteLine("button touch");
            }
        }
    }
    
    public override void Draw() {}
}