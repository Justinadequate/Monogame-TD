using TDGame.Models;
using TDGame.Util;

namespace TDGame.Components.Ui;
public class UiItem : Component
{
    public UiItemType ItemType { get; set; }
    public string Text { get; set; }

    public UiItem(UiItemType itemType, string text = "", bool active = true) : base(ComponentTypes.Ui_UiItem, active)
    {
        ItemType = itemType;
        Text = text;
    }
}