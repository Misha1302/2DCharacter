namespace Interfaces
{
    public interface IJumpable
    {
        public void Updating();
        public void Init(IAnimated animated, IMovable movable);
        public bool IsGrounded();
    }
}