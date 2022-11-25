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
        new SceneManager(_spriteBatch, new[] {
            new Scene1("scene1", true, Content)
        });
        _drawingSystem = new DrawingSystem(_spriteBatch);
        _enemySystem = new EnemySystem();
        _collisionSystem = new CollisionSystem();

        SceneManager.Instance.LoadContent(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.KeyBoardState = Keyboard.GetState();
        Globals.MouseState = Mouse.GetState();

        // TODO: Move update stuff into Scene management
        _drawingSystem.Update();
        _camera.Update(gameTime);
        _collisionSystem.Update();
        _enemySystem.Update();
        base.Update(gameTime);

        Globals.PreviousKeyBoardState = Globals.KeyBoardState;
        Globals.PreviousMouseState = Globals.MouseState;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Move draw stuff into Scene management
        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        _drawingSystem.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
