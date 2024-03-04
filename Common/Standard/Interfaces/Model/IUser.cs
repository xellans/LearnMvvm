namespace Common.Standard.Interfaces.Model
{
    // В интерфейсе все свойства "только для чтения".
    public interface IUser : IId
    {
        //public long Id { get; /*set;*/ } Это свойтсво уже есть в IId

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get;/* set;*/ }
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get; /*set;*/ }
    }

    public struct UserDto : IUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAuthorized { get; set; }

        public UserDto(IUser user)
        {
            Id = user.Id;
            Name = user.Name;
            IsAuthorized = user.IsAuthorized;
        }
    }
}
