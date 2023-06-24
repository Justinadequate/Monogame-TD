using System.Diagnostics;
using System.Diagnostics.Tracing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TDGame.Models;
using TDGame.Systems;
using TDGame.Util;

namespace TDGame.Scenes.DefinedScenes;
public class EditorScene : Scene
{
    private TileType SelectedTileType;
    private Map Map;
    private Texture2D Rectangle;
    public EditorScene(string name, bool active, ContentManager content, params ISystem[] systems) : base(name, active, content, systems) { }

    public override void Initialize(GraphicsDeviceManager graphics)
    {
        Map = new Map("newMap", 50);
        graphics.PreferredBackBufferWidth = 16 * Map.TileSize;
        graphics.PreferredBackBufferHeight = 16 * Map.TileSize;
        graphics.ApplyChanges();

        Rectangle = new Texture2D(graphics.GraphicsDevice, 1, 1);
        Rectangle.SetData<Color>(new Color[] {Color.White});
    }

    public override void LoadContent(SpriteBatch spriteBatch)
    {
        var pos = Vector2.Zero;
        for (int x = 0; x < Map.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Map.Tiles.GetLength(1); y++)
            {
                Map.Tiles[x, y].Type = TileType.Empty;
                Map.Tiles[x, y].Bounds = new Rectangle((int)pos.X, (int)pos.Y, Map.TileSize, Map.TileSize);
                pos.X += Map.TileSize;
            }
            pos.X = 0;
            pos.Y += Map.TileSize;
        }
    }

    public override void Update(float deltaTime)
    {
        // Key names from 1 to 0:
        // Apps
        // Sleep
        // NumPad0
        // NumPad1
        // NumPad2
        // NumPad3
        // NumPad4
        // NumPad5
        // RightWindows

        // TODO: tile type not updating, keyboard state not getting
        if (Globals.KeyBoardState.IsKeyDown(Keys.Apps))
            SelectedTileType = TileType.DirtPath;
        else if (Globals.KeyBoardState.IsKeyDown(Keys.Sleep))
            SelectedTileType = TileType.Grass;
        else if (Globals.KeyBoardState.IsKeyDown(Keys.NumPad0))
            SelectedTileType = TileType.Water;
        else if (Globals.KeyBoardState.IsKeyDown(Keys.NumPad1))
            SelectedTileType = TileType.Mortar;

        var mousePos = new Rectangle(Globals.MouseState.Position, new Point(1, 1));

        if (Globals.MouseState.LeftButton == ButtonState.Pressed)
        {
            for (int x = 0; x < Map.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Map.Tiles.GetLength(1); y++)
                {
                    if (Map.Tiles[x, y].Bounds.Intersects(mousePos))
                        Map.Tiles[x, y].Type = SelectedTileType;
                }
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Color color = Color.White;
        for (int x = 0; x < Map.Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Map.Tiles.GetLength(1); y++)
            {
                if (Map.Tiles[x,y].Type == TileType.Empty)
                    color = Color.Black;
                if (Map.Tiles[x,y].Type == TileType.DirtPath)
                    color = Color.Beige;
                if (Map.Tiles[x,y].Type == TileType.Grass)
                    color = Color.LawnGreen;
                if (Map.Tiles[x,y].Type == TileType.Water)
                    color = Color.AliceBlue;
                if (Map.Tiles[x,y].Type == TileType.Mortar)
                    color = Color.SlateGray;

                spriteBatch.Draw(
                    texture: Rectangle,
                    position: Map.Tiles[x,y].Bounds.Location.ToVector2(),
                    color: color);
            }
        }
    }

    public override void UnloadContent()
    {
        throw new System.NotImplementedException();
    }
}