using Entity;

namespace Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        bool IsExistName(string name);
    }
}
