namespace Common
{
    public interface IUser
    {
        long Id { get; }
        string Name { get; }
        bool IsAuthorized { get; }
    }

    public class UserBase : IUser
    {
        protected long _id;
        protected string _name = string.Empty;
        protected bool _isAuthorized;
        long IUser.Id => _id;
        string IUser.Name => _name;
        bool IUser.IsAuthorized => _isAuthorized;
    }
}
