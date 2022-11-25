namespace TDGame.Scenes;
public interface IScene
{
    string Name { get; set; }
    bool Active { get; set; }
}