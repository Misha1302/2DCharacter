namespace Interfaces
{
    public interface IMovable
    {
        public void Updating();
        public float HorizontalMovement();
        public void Init(IAnimated animated, IJumpable jumpable, IFightable fightable);
    }
}