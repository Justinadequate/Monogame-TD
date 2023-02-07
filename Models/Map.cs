using Microsoft.Xna.Framework;

namespace TDGame.Models;
public class Map
{
    public string Name { get; set; }
    public int RowCount { get; set; }
    public int ColumnCount { get; set; }
    public int TileSize { get; set; }
    public MapTile[,] Tiles { get; set; }

    public Map(string name, int tileSize, int rowCount = 50, int columnCount = 50)
    {
        Name = name;
        TileSize = tileSize;
        RowCount = rowCount;
        ColumnCount = columnCount;

        Tiles = new MapTile[rowCount, columnCount];
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                Tiles[x,y] = new MapTile();
            }
        }
    }
}

public class MapTile
{
    public Rectangle Bounds { get; set; }
    public TileType Type { get; set; }

    public MapTile()
    {
        Bounds = new Rectangle();
        Type = TileType.Empty;
    }
}

public enum TileType
{
    Empty = 0,
    DirtPath = 1,
    Grass = 2,
    Water = 3,
    Mortar = 4
}