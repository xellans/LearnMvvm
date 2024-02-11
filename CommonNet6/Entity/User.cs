namespace Entity
{
    /// <summary>Это DTO, а не сущности. Поэтому нужна настройка БД, если их использовать в EF.</summary>
    public class User 
    {
        public long Id { get; set; }

        /// <summary>Имя пользователя.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized  { get; set; }
    }

}
