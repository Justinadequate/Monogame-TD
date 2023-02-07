using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Assets;
using TDGame.Components;
using TDGame.Models;
using TDGame.Systems;
using TDGame.Util;

namespace TDGame.Scenes.DefinedScenes;
public class Scene1 : Scene
{
    public Scene1(string name, bool active, ContentManager content, params ISystem[] systems) : base(name, active, content, systems) {}

    public override void Initialize(GraphicsDeviceManager graphics)
    {
        graphics.PreferredBackBufferWidth = 16 * Maps.Map1.GetLength(0);
        graphics.PreferredBackBufferHeight = 16 * Maps.Map1.GetLength(1);
        graphics.ApplyChanges();
    }

    public override void LoadContent(SpriteBatch spriteBatch)
    {
        //* Load Map
        var pos = Vector2.Zero;
        var tileSize = 16;
        var tileName = "";
        for (int x = 0; x < Maps.Map1.GetLength(0); x++)
        {
            var texTerrain = Content.Load<Texture2D>(Textures.Asset_AllTerrain);
            for (int y = 0; y < Maps.Map1.GetLength(1); y++)
            {
                if (Maps.Map1[x,y].Terrain == Textures.SourceR_Grass)
                    tileName = "tile_grass";
                if (Maps.Map1[x,y].Terrain == Textures.SourceR_Mortar)
                    tileName = "tile_mortar";
                if (Maps.Map1[x,y].Terrain == Textures.SourceR_Water)
                    tileName = "tile_water";
                if (Maps.Map1[x,y].Terrain == Textures.SourceR_Dirt)
                    tileName = "tile_dirt";

                Entity tile = new Entity(tileName);
                var tileRendering = new Rendering(texTerrain, Maps.Map1[x,y].Terrain);
                var tileTransform = new Transform(pos.ToPoint(), tileRendering.Source.Size);
                var tileTile = new Tile(Globals.DirectionsMap.GetValueOrDefault(Maps.Map1[x,y].MoveDirection));
                tile.AddComponents(tileTransform);
                tile.AddComponents(tileRendering);
                tile.AddComponents(tileTile);
                
                if (tile.Name == "tile_dirt" || tile.Name == "tile_mortar")
                {
                    tile.AddComponents(new Collider(tileTransform.Destination, CollisionLayer.World, CollisionLayer.Enemy));

                    if (tile.Name == "tile_mortar")
                        tileTile.IsStart = true;
                }

                pos.X += tileSize;
            }
            pos.X = 0;
            pos.Y += tileSize;
        }

        Entity monster = new Entity("monster");
        var monsterSprite = Content.Load<Texture2D>(Textures.Asset_Monster1);
        var monsterStart = EntityManager.Instance.GetEntities().FirstOrDefault(e => e.Name == "tile_mortar")
            .GetComponent<Transform>().Destination.Location;
        var monsterTransform = new Transform(monsterStart, monsterSprite.Bounds.Size);
        var monsterRendering = new Rendering(Content.Load<Texture2D>(Textures.Asset_Monster1), Textures.SourceR_Monster);
        var monsterCollider = new Collider(
            monsterTransform.Destination, CollisionLayer.Enemy, CollisionLayer.World);
        monster.AddComponents(monsterTransform);
        monster.AddComponents(monsterRendering);
        monster.AddComponents(new Enemy(5, 200f));
        monster.AddComponents(monsterCollider);
    }

    public override void UnloadContent()
    {
        throw new NotImplementedException();
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
}