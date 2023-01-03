using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDGame.Scenes;
public class SceneManager
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    public static SceneManager Instance { get; set; }
    public Scene CurrentScene { get; set; }

    public SceneManager(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, params Scene[] scenes)
    {
        _graphics = graphics;
        _spriteBatch = spriteBatch;
        _scenes = scenes.ToDictionary(s => s.Name);
        Instance = this;
        CurrentScene = scenes.FirstOrDefault();
    }

    public void Initialize()
    {
        if (CurrentScene is not null)
            CurrentScene.Initialize(_graphics);
    }

    public void LoadContent()
    {
        if (CurrentScene is not null)
            CurrentScene.LoadContent(_spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        if (CurrentScene is not null)
            CurrentScene.Update((float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0));
    }

    public void Draw()
    {
        if (CurrentScene is not null)
            CurrentScene.Draw(_spriteBatch);
    }

    // TODO: add methods for unloading and changing scenes
    public void ChangeScene(string sceneName)
    {
        if (CurrentScene is not null)
            CurrentScene.UnloadContent();
        
        CurrentScene = _scenes[sceneName];
        CurrentScene.Initialize(_graphics);
        CurrentScene.LoadContent(_spriteBatch);
    }

    public void AddScene(Scene scene) => _scenes.Add(scene.Name, scene);
}