using Common.Standard.Interfaces.Model;
using System.Collections.Generic;

namespace DataBase
{
    public class User : IUser
    {
        public virtual long Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public virtual bool IsAuthorized { get; set; }
    }

}
