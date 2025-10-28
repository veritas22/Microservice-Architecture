namespace Grps.Services
{
    public interface IAccountServise
    {
        Task<int> AddAccount(int userId);
    }
}