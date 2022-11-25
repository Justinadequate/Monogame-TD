using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Scenes;
public abstract class Scene : IScene
{
    public string Name { get; set; }
    public bool Active { get; set; }
    public ContentManager Content { get; set; }

    public Scene(string name, bool active, ContentManager content)
    {
        Name = name;
        Active = active;
        Content = content;
    }

    public abstract void Initialize();
    public abstract void LoadContent(SpriteBatch spriteBatch);
    public abstract void UnloadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}