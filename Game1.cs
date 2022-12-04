using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using TDGame.Extensions;
using TDGame.Scenes;
using TDGame.Systems;

namespace TDGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;
    private DrawingSystem _drawingSystem;
    private EnemySystem _enemySystem;
    private CollisionSystem _collisionSystem;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //* Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 16*50;
        _graphics.PreferredBackBufferHeight = 16*50;
        _graphics.ApplyChanges();

        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _camera = new OrthographicCamera(viewportAdapter);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        new EntityManager();
        _drawingSystem = new DrawingSystem(_spriteBatch);
        _enemySystem = new EnemySystem();
        _collisionSystem = new CollisionSystem();
        
        new SceneManager(_spriteBatch, new[] {
            new Scene1("scene1", true, Content,
            _drawingSystem,
            _enemySystem,
            _collisionSystem)
        });
        SceneManager.Instance.LoadContent(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.KeyBoardState = Keyboard.GetState();
        Globals.MouseState = Mouse.GetState();

        _camera.Update(gameTime);
        SceneManager.Instance.CurrentScene.Update(gameTime);
        base.Update(gameTime);

        Globals.PreviousKeyBoardState = Globals.KeyBoardState;
        Globals.PreviousMouseState = Globals.MouseState;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        SceneManager.Instance.CurrentScene.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
