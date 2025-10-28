using Domen;

namespace Application.Interfaces
{
    public interface IEFStore
    {
        Task Migrate(CancellationToken cancel);
    }
}
