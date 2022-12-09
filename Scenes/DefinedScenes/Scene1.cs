using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Assets;
using TDGame.Components;
using TDGame.Systems;
using TDGame.Util;

namespace TDGame.Scenes;
public class Scene1 : Scene
{
    public Scene1(string name, bool active, ContentManager content, params ISystem[] systems) : base(name, active, content, systems) {}

    public override void Initialize()
    {
        throw new NotImplementedException();
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
                var tileTransform = new Transform(pos);
                var tileRendering = new Rendering(texTerrain, Maps.Map1[x,y].Terrain);
                var tileTile = new Tile(Globals.DirectionsMap.GetValueOrDefault(Maps.Map1[x,y].MoveDirection));
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

                pos.X += tileSize;
            }
            pos.X = 0;
            pos.Y += tileSize;
        }

        Entity monster = new Entity("monster");
        var monsterStart = EntityManager.Instance.GetEntities().FirstOrDefault(e => e.Name == "tile_mortar")
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

    public override void UnloadContent()
    {
        throw new NotImplementedException();
    }

    public override void Update(GameTime gameTime)
    {
        for (int i = 0; i < Systems.Count; i++)
            Systems[i].Update();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Systems.Count; i++)
            Systems[i].Draw();
    }
}