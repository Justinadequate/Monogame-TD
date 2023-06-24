using System;
using TDGame.Util;

namespace TDGame.Components.Ui;
public class Clickable : Component
{
    public Action OnClick { get; set; }

    public Clickable(Action onClick, bool active = true) : base(ComponentTypes.Ui_Clickable, active)
    {
        OnClick = onClick;
    }
}