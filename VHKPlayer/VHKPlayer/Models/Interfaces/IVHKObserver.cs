namespace VHKPlayer.Models.Interfaces
{
    public interface IVhkObserver<T>
    {
        void SubjectUpdated(T subject);
    }
}