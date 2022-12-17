using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Components;

namespace TDGame.Systems;
public class DrawingSystem : System<Rendering>
{
    private SpriteBatch _spriteBatch;

    public DrawingSystem(SpriteBatch spriteBatch) : base()
    {
        _spriteBatch = spriteBatch;
    }

    public override void Update(float deltaTime) {}

    public override void Draw()
    {
        for (int i = 0; i < Components.Count; i++)
        {
            var transform = Components[i].Entity.GetComponent<Transform>();
            _spriteBatch.Draw(
                texture: Components[i].Texture,
                destinationRectangle: transform.Destination,
                sourceRectangle: Components[i].Source,
                color: Components[i].DrawColor,
                rotation: transform.Rotation,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: Components[i].Layer
            );
        }
    }
}