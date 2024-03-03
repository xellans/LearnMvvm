namespace Common.Standard.Interfaces.Model
{
    public interface IUser : IId
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get; }
    }
}
