using System;

namespace TDGame.Components.Ui;
public class Clickable : Component
{
    public Action OnClick { get; set; }

    public Clickable(Action onClick, bool active = true) : base(active)
    {
        OnClick = onClick;
    }
}