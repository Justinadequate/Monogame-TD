namespace TDGame.Components;
public class Component
{
    public int Id { get; }
    public Entity Entity { get; set; }
    public bool Active { get; set; }

    public Component(bool active)
    {
        Active = active;
    }
}