using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace TDGame.Extensions
{
    public static class OrthographicCameraExtensions
    {
        public static void Update(this OrthographicCamera camera, GameTime gameTime)
        {
            float cameraSpeed = 200;
            float zoomSpeed = 0.2f;

            if (Globals.MouseState.ScrollWheelValue > Globals.PreviousMouseState.ScrollWheelValue)
                camera.ZoomIn(zoomSpeed);
            if (Globals.MouseState.ScrollWheelValue < Globals.PreviousMouseState.ScrollWheelValue)
                camera.ZoomOut(zoomSpeed);
            camera.Move(GetMovementDirection() * cameraSpeed * gameTime.GetElapsedSeconds());

            Vector2 GetMovementDirection()
            {
                var movementDirection = Vector2.Zero;
                var state = Globals.KeyBoardState;
                if (state.IsKeyDown(Keys.Down))
                    movementDirection += Vector2.UnitY;
                if (state.IsKeyDown(Keys.Up))
                    movementDirection -= Vector2.UnitY;
                if (state.IsKeyDown(Keys.Left))
                    movementDirection -= Vector2.UnitX;
                if (state.IsKeyDown(Keys.Right))
                    movementDirection += Vector2.UnitX;
                return movementDirection;
            }
        }
    }
}