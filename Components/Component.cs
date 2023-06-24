namespace TDGame.Components;
public class Component
{
    public string Type { get; }
    public int Id { get; }
    public Entity Entity { get; set; }
    public bool Active { get; set; }

    public Component(string type, bool active)
    {
        Type = type;
        Active = active;
    }
}