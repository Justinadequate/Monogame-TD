using Microsoft.Xna.Framework;

namespace TDGame.Util;
public static class Textures
{
    #region Map Tiles
    public const string Asset_AllTerrain = "Terrain/terrain";
    public static Rectangle SourceR_Grass = new Rectangle(1, 1, 16, 16);
    public static Rectangle SourceR_Mortar = new Rectangle(19, 1, 16, 16);
    public static Rectangle SourceR_Water = new Rectangle(1, 19, 16, 16);
    public static Rectangle SourceR_Dirt = new Rectangle(19, 19, 16, 16);
    #endregion

    #region Enemies
    public const string Asset_Monster1 = "Enemies/monster1";
    public static Rectangle SourceR_Monster = new Rectangle(1, 1, 16, 16);
    #endregion

    #region UI
    public const string Asset_Button = "UI/button";
    public static Rectangle SourceR_Button = new Rectangle(1, 1, 100, 50);
    #endregion
}