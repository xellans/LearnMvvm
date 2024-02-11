namespace Interfaces
{

    public interface IPeopleModel
    {
        IUsersRepository UsersRepository { get; }
        IPeopleRepository PeopleRepository { get; }
    }
}
