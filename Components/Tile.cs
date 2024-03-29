using Microsoft.Xna.Framework;
using TDGame.Util;

namespace TDGame.Components;
public class Tile : Component
{
    public bool IsStart { get; set; }
    public bool IsFinish { get; set; }
    public Vector2 MoveDirection { get; set; }
    public float MoveModifier { get; set; }

    public Tile(
        Vector2 moveDirection,
        bool isStart = false,
        bool isFinish = false,
        bool active = true) : base(ComponentTypes.Tile, active)
    {
        MoveDirection = moveDirection;
        IsStart = isStart;
        IsFinish = isFinish;
    }
}