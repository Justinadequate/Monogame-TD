using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Systems;

namespace TDGame.Scenes;
public abstract class Scene : IScene
{
    public string Name { get; set; }
    public bool Active { get; set; }
    public ContentManager Content { get; set; }
    public List<ISystem> Systems { get; set; }

    public Scene(string name, bool active, ContentManager content, params ISystem[] systems)
    {
        Name = name;
        Active = active;
        Content = content;
        
        Systems = new List<ISystem>();
        if (systems.Any())
            Systems.AddRange(systems);
    }

    public abstract void Initialize(GraphicsDeviceManager graphics);
    public abstract void LoadContent(SpriteBatch spriteBatch);
    public abstract void UnloadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}