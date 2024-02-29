using Common.Standard.Interfaces.Model;
using System.Collections.Generic;

namespace DataBase
{
    public class User : IUser
    {
        public long Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get; set; }
    }

}
