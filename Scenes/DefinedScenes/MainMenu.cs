using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Components;
using TDGame.Models;
using TDGame.Systems;
using TDGame.Util;

namespace TDGame.Scenes;
public class MainMenu : Scene
{
    public MainMenu(string name, bool active, ContentManager content, params ISystem[] systems) : base(name, active, content, systems) {}

    public override void Initialize(GraphicsDeviceManager graphics) {}

    public override void LoadContent(SpriteBatch spriteBatch)
    {
        Entity button = new Entity("button");
        var uiItem = new UiItem(UiItemType.Button, "test");
        var texture = Content.Load<Texture2D>(Textures.Asset_Button);
        var rendering = new Rendering(texture, Textures.SourceR_Button);
        var transform = new Transform(new Vector2(100, 100));
        var collider = new Collider(
            new Rectangle(
                (int)transform.Position.X,
                (int)transform.Position.Y,
                (int)Math.Floor(rendering.Source.Width * transform.Scale.X),
                (int)Math.Floor(rendering.Source.Height * transform.Scale.Y)
            ), transform.Position, CollisionLayer.Ui, CollisionLayer.Ui);
        button.AddComponents(rendering, transform, uiItem, collider);

        Entity cursor = new Entity("cursor");
        uiItem = new UiItem(UiItemType.Cursor);
        texture = Content.Load<Texture2D>(Textures.Asset_Cursor);
        rendering = new Rendering(texture, Textures.SourceR_Cursor);
        transform = new Transform(new Vector2(500, 500));
        collider = new Collider(
            new Rectangle(
                (int)transform.Position.X,
                (int)transform.Position.Y,
                1, 1
            ), transform.Position, CollisionLayer.Ui, CollisionLayer.Ui);
        cursor.AddComponents(rendering, transform, uiItem, collider);
    }

    public override void Update(float deltaTime)
    {
        for (int i = 0; i < Systems.Count; i++)
            Systems[i].Update(deltaTime);
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Systems.Count; i++)
            Systems[i].Draw();
    }

    public override void UnloadContent()
    {
        throw new System.NotImplementedException();
    }
}