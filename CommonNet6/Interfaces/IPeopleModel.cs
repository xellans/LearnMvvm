using Entity;

namespace Interfaces
{

    public interface IPeopleModel
    {
        protected IUsersRepository UsersRepository { get; }
        IPeopleRepository PeopleRepository { get; }

        bool IsAuthorized { get; }
        event EventHandler<IsAuthorizedChangedEventArgs>? AuthorizedChanged;

        /// <summary>Авторизует пользователя и запоминает его для следующего сенса.</summary>
        /// <param name="user">Авторизуемый пользователь. Если <see langword="null"/> - сброс авторизацции.</param>
        void Authorize(string? userName);
    }

    public class IsAuthorizedChangedEventArgs : EventArgs
    {
        public bool IsAuthorized { get; }

        public IsAuthorizedChangedEventArgs(bool isAuthorized)
        {
            IsAuthorized = isAuthorized;
        }
    }
}
