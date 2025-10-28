using Entity.Dto;
using Entity.User;

namespace Interfases
{
    public interface IEFStore
    {
        Task Migrate(CancellationToken cancel);

        Task AddUser(UserDto user, CancellationToken cancel);

        Task DeleteUser(int userId, CancellationToken cancel);

        Task UpdateUser(int userId, UserDto userDto, CancellationToken cancel);
        Task<User> GetUser(int userId, CancellationToken cancel);

    }
}
