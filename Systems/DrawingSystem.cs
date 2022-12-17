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

    public override void Update(float deltaTime)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            // TODO: Move destination rectangle into renderer component?
            var transform = Components[i].Entity.GetComponent<Transform>();
            transform.Destination = new Rectangle(
                (int)transform.Position.X,
                (int)transform.Position.Y,
                (int)Math.Floor(Components[i].Source.Width * transform.Scale.X),
                (int)Math.Floor(Components[i].Source.Height * transform.Scale.Y)
            );
        }
    }

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