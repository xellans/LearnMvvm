using Common;

namespace DataBase.Entity
{
    internal class User : UserBase, IUser
    {
        public long Id { get => _id; set => _id = value; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get => _isAuthorized; set => _isAuthorized = value; }
    }

}
