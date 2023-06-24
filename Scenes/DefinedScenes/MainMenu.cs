using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Components;
using TDGame.Components.Ui;
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
        var texture = Content.Load<Texture2D>(Textures.Asset_Button);
        var rendering = new Rendering(texture, Textures.SourceR_Button);
        var transform = new Transform(new Point(100, 100), rendering.Source.Size);

        var button1 = new Entity("button1")
            .AddComponents(
                new Rendering(rendering),
                new Transform(transform),
                new Collider(transform.Destination, CollisionLayer.Ui, CollisionLayer.Ui),
                new UiItem(UiItemType.Button, "test"),
                new Clickable(() => SceneManager.Instance.ChangeScene("scene1"))
            );
        var button2 = new Entity("button2")
            .AddComponents(
                new Rendering(rendering),
                new Transform(transform.Destination.Location + new Point(500, 0) , rendering.Source.Size),
                new Collider(
                    new Rectangle(transform.Destination.Location + new Point(500, 0), transform.Destination.Size),
                    CollisionLayer.Ui, CollisionLayer.Ui),
                new UiItem(UiItemType.Button, "test2"),
                new Clickable(() => SceneManager.Instance.ChangeScene("editor"))
            );

        texture = Content.Load<Texture2D>(Textures.Asset_Cursor);
        rendering = new Rendering(texture, Textures.SourceR_Cursor);

        var cursor = new Entity("cursor")
            .AddComponents(
                new Rendering(texture, Textures.SourceR_Cursor),
                new Transform(new Point(500, 500), rendering.Source.Size),
                new UiItem(UiItemType.Cursor),
                new Collider(
                    new Rectangle(
                        (int)transform.Destination.X,
                        (int)transform.Destination.Y,
                        1, 1
                    ), CollisionLayer.Ui, CollisionLayer.Ui)
            );
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
        var entities = EntityManager.Instance.GetEntities();
        for (int i = 0; i < entities.Count; i++)
            entities[i].Destroy();
        
        Content.Unload();
    }
}