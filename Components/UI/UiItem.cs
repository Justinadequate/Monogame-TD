using TDGame.Models;

namespace TDGame.Components.Ui;
public class UiItem : Component
{
    public UiItemType ItemType { get; set; }
    public string Text { get; set; }

    public UiItem(UiItemType itemType, string text = "", bool active = true) : base(active)
    {
        ItemType = itemType;
        Text = text;
    }
}