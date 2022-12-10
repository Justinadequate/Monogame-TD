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
            // TODO: onclick/onhover/onwhatever
            // UI might need its own entity structure to handle events
            // try desperately to remember your frontend experience
        }
    }
    
    public override void Draw() {}
}