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
        var buttonTexture = Content.Load<Texture2D>(Textures.Asset_Button);
        var rendering = new Rendering(buttonTexture);
        var transform = new Transform(new Vector2(100, 100));
        var components = new Component[]
        {
            transform,
            rendering,
            // TODO: collider wonky find better way to initialize
            new Collider(new Rectangle(
                (int)transform.Position.X,
                (int)transform.Position.Y,
                rendering.Source.Width,
                rendering.Source.Height
            ), new Vector2(100, 100)),
            new UiItem(UiItemType.Button, "test")
        };

        button.AddComponents(components);
    }

    public override void Update(GameTime gameTime)
    {
        for (int i = 0; i < Systems.Count; i++)
            Systems[i].Update();
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