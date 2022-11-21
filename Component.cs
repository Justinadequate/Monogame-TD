using System.Collections.Generic;

namespace TDGame
{
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
}