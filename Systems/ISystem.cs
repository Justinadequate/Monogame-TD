namespace TDGame.Systems;

public interface ISystem
{
    void Update(float deltaTime);
    void Draw();
}