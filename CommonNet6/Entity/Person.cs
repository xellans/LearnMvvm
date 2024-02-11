namespace Entity
{
    /// <summary>Это DTO, а не сущности. Поэтому нужна настройка БД, если их использовать в EF.</summary>
    public class Person
    {
        public long Id { get; set; }

        /// <summary>Имя человека.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Возраст человека.</summary>
        public int Age { get; set; }
    }

}
