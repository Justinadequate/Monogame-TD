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
    // private OrthographicCamera _camera;
    private DrawingSystem _drawingSystem;
    private EnemySystem _enemySystem;
    private CollisionSystem _collisionSystem;
    private UiSystem _uiSystem;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        new EntityManager();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _drawingSystem = new DrawingSystem(_spriteBatch);
        _enemySystem = new EnemySystem();
        _collisionSystem = new CollisionSystem();
        _uiSystem = new UiSystem();

        var scenes = new Scene[] {
            new MainMenu("main-menu", true, Content,
                _drawingSystem,
                _collisionSystem,
                _uiSystem),
            new Scene1("scene1", true, Content,
                _drawingSystem,
                _enemySystem,
                _collisionSystem)
        };
        new SceneManager(_spriteBatch, scenes);

        SceneManager.Instance.Initialize(_graphics);
        
        // var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        // _camera = new OrthographicCamera(viewportAdapter);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SceneManager.Instance.LoadContent(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.KeyBoardState = Keyboard.GetState();
        Globals.MouseState = Mouse.GetState();

        // _camera.Update(gameTime);
        SceneManager.Instance.Update(gameTime);
        base.Update(gameTime);

        Globals.PreviousKeyBoardState = Globals.KeyBoardState;
        Globals.PreviousMouseState = Globals.MouseState;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SceneManager.Instance.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
