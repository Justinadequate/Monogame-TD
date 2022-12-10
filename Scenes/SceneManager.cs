using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Scenes;
public class SceneManager
{
    public static SceneManager Instance { get; set; }
    public Scene CurrentScene { get; set; }
    private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

    public SceneManager(SpriteBatch spriteBatch, params Scene[] scenes)
    {
        Instance = this;
        CurrentScene = scenes.FirstOrDefault();
        _scenes = scenes.ToDictionary(s => s.Name);
    }

    public void Initialize(GraphicsDeviceManager graphics)
    {
        if (CurrentScene is not null)
            CurrentScene.Initialize(graphics);
    }

    public void LoadContent(SpriteBatch spriteBatch)
    {
        if (CurrentScene is not null)
            CurrentScene.LoadContent(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        if (CurrentScene is not null)
            CurrentScene.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (CurrentScene is not null)
            CurrentScene.Draw(spriteBatch);
    }

    // TODO: add methods for unloading and changing scenes

    public void AddScene(Scene scene) => _scenes.Add(scene.Name, scene);
}