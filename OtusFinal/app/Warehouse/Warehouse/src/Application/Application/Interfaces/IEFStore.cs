using Domain;

namespace Interfases
{
    public interface IEFStore
    {
        Task Migrate(CancellationToken cancel);
        Task<IEnumerable<Reserve>> GetReserve(int id, CancellationToken cancel);

        Task<int> AddReserve(Reserve reserve, CancellationToken cancel);
        Task<int> DeleteReserve(Reserve reserve, CancellationToken cancel);
    }
}
