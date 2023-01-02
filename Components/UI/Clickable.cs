using System;

namespace TDGame.Components.Ui;
public class Clickable : Component
{
    public Action<Entity> OnClick { get; set; }

    public Clickable(Action<Entity> onClick, bool active = true) : base(active)
    {
        OnClick = onClick;
    }
}