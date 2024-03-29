using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDGame.Util;

namespace TDGame.Components;
public class Rendering : Component
{
    public Texture2D Texture { get; set; }
    public Color DrawColor { get; set;}
    public Rectangle Source { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public float Layer { get; set; }

    public Rendering(Texture2D texture, bool active = true) : base(ComponentTypes.Rendering, active)
    {
        Texture = texture;
        Source = texture.Bounds;
        DrawColor = Color.White;
    }

    public Rendering(Texture2D texture, Rectangle source, bool active = true) : base(ComponentTypes.Rendering, active)
    {
        Texture = texture;
        Source = source;
        DrawColor = Color.White;
    }

    public Rendering(GraphicsDevice graphics, Color color, bool active = true) : base(ComponentTypes.Rendering, active)
    {
        Texture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
        Texture.SetData<Color>(new Color[] {color});
        Source = new Rectangle(100, 100, 50, 50);
    }

    public Rendering(Rendering rendering, bool active = true) : base(ComponentTypes.Rendering, active)
    {
        Texture = rendering.Texture;
        DrawColor = rendering.DrawColor;
        Source = rendering.Source;
        Height = rendering.Height;
        Width = rendering.Width;
        Layer = rendering.Layer;
    }
}