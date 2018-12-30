namespace VHKPlayer.Queries.Interfaces
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}