using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using TDGame.Assets;
using TDGame.Components;
using TDGame.Extensions;
using TDGame.Systems;
using TDGame.Util;

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

        //* Load Map
        var pos = Vector2.Zero;
        var tileSize = 16;
        var tileName = "";
        for (int x = 0; x < Maps.Map2.GetLength(0); x++)
        {
            var texTerrain = Content.Load<Texture2D>(Textures.Asset_AllTerrain);
            for (int y = 0; y < Maps.Map2.GetLength(1); y++)
            {
                if (Maps.Map2[x,y].Terrain == Textures.SourceR_Grass)
                    tileName = "tile_grass";
                if (Maps.Map2[x,y].Terrain == Textures.SourceR_Mortar)
                    tileName = "tile_mortar";
                if (Maps.Map2[x,y].Terrain == Textures.SourceR_Water)
                    tileName = "tile_water";
                if (Maps.Map2[x,y].Terrain == Textures.SourceR_Dirt)
                    tileName = "tile_dirt";

                Entity tile = new Entity(tileName);
                var tileTransform = new Transform(pos);
                var tileRendering = new Rendering(texTerrain, Maps.Map2[x,y].Terrain);
                var tileTile = new Tile(Globals.DirectionsMap.GetValueOrDefault(Maps.Map2[x,y].MoveDirection));
                tile.AddComponent(tileTransform);
                tile.AddComponent(tileRendering);
                tile.AddComponent(tileTile);
                
                if (tile.Name == "tile_dirt" || tile.Name == "tile_mortar")
                {
                    var tileBounds = new Rectangle(
                        (int)tileTransform.Position.X,
                        (int)tileTransform.Position.Y,
                        (int)Math.Floor(tileRendering.Source.Width * tileTransform.Scale.X),
                        (int)Math.Floor(tileRendering.Source.Height * tileTransform.Scale.Y)
                    );
                    var tilePos = tile.GetComponent<Transform>().Position;
                    tile.AddComponent(new Collider(tileBounds, tilePos));

                    if (tile.Name == "tile_mortar")
                        tile.GetComponent<Tile>().IsStart = true;
                }

                // if (Maps.Map2[x,y].Terrain == Textures.SourceR_Mortar)
                //     tile.GetComponent<Tile>().IsStart = true;

                pos.X += tileSize;
            }
            pos.X = 0;
            pos.Y += tileSize;
        }

        Entity monster = new Entity("monster");
        var monsterStart = EntityManager.Instance.Entities.FirstOrDefault(e => e.Name == "tile_mortar")
            .GetComponent<Transform>().Position;
        var monsterTransform = new Transform(monsterStart);
        var monsterRendering = new Rendering(Content.Load<Texture2D>(Textures.Asset_Monster1), Textures.SourceR_Monster);
        var monsterCollider = new Collider(
            new Rectangle(
                (int)monsterTransform.Position.X,
                (int)monsterTransform.Position.Y,
                (int)Math.Floor(monsterRendering.Source.Width * monsterTransform.Scale.X),
                (int)Math.Floor(monsterRendering.Source.Height * monsterTransform.Scale.Y)
            ),
            monsterTransform.Position);
        monster.AddComponent(monsterTransform);
        monster.AddComponent(monsterRendering);
        monster.AddComponent(new Enemy(5, 2f));
        monster.AddComponent(monsterCollider);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.KeyBoardState = Keyboard.GetState();
        Globals.MouseState = Mouse.GetState();

        //* Add your update logic here

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

        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        // TODO: monster movement works but Draw only being called every now and then? either that or it's going extremely slow
        _drawingSystem.Draw();
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
