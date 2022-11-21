using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TDGame.Assets;

namespace TDGame
{
    public static class Globals
    {
        public static bool Pause { get; set; }
        public static KeyboardState KeyBoardState { get; set; }
        public static KeyboardState PreviousKeyBoardState { get; set; }
        public static MouseState MouseState { get; set; }
        public static MouseState PreviousMouseState { get; set; }
        
        public static Dictionary<Direction, Vector2> DirectionsMap = new Dictionary<Direction, Vector2>()
        {
            {Direction.Up, new Vector2(0, -1)},
            {Direction.Right, new Vector2(1, 0)},
            {Direction.Down, new Vector2(0, 1)},
            {Direction.Left, new Vector2(-1, 0)}
        };
    }
}