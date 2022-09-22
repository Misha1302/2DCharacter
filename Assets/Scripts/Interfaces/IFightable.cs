namespace Interfaces
{
    public interface IFightable
    {
        public void Updating();
        public bool IsFighting();
        public void Init(IAnimated animated);
    }
}