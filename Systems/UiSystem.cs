using System.Diagnostics;
using Microsoft.Xna.Framework;
using TDGame.Components;

namespace TDGame.Systems;
public class UiSystem : System<UiItem>
{
    public UiSystem() : base() {}

    public override void Update()
    {
        for (int i = 0; i < Components.Count; i++)
        {
            var collider = Components[i].Entity.GetComponent<Collider>();
            var renderer = Components[i].Entity.GetComponent<Rendering>();
            if (collider.Bounds.Intersects(
                new Rectangle(Globals.MouseState.X, Globals.MouseState.Y, 1, 1)))
                Debug.WriteLine("menu item touched");
        }
    }
    
    public override void Draw() {}
}