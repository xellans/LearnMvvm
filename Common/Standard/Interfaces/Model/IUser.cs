﻿namespace Common.Standard.Interfaces.Model
{
    public interface IUser : IId
    {
        public long Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Возвращает true если пользователь авторизирован
        /// </summary>
        public bool IsAuthorized { get; set; }
    }
}
