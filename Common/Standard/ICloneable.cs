﻿using System.Reflection;

namespace Standard
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
    public static partial class CloneableHelper
    {
        /// <summary>Создаёт клон экземпляра класса <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">Тип экземплярв.</typeparam>
        /// <param name="obj">Исходный экземпляр.</param>
        /// <returns>Возвращает новый экземпляр типа <typeparamref name="T"/>
        /// являющийся копией исходного экземляра.</returns>
        /// <remarks>Если класс <typeparamref name="T"/> реализует интерфейс <see cref="ICloneable"/>,
        /// то копия создаётся методом <see cref="ICloneable.Clone"/>.
        /// Иначе - методом 
        /// <a href="https://docs.microsoft.com/ru-ru/dotnet/api/system.object.memberwiseclone?view=net-5.0">
        /// Object.MemberwiseClone()</a>.</remarks>
        public static T Clone<T>(this T obj)
        {
            T clone;
            if (obj is ICloneable<T> clnT)
            {
                clone = clnT.Clone();
            }
            else if (obj is ICloneable cln && cln.Clone() is T t)
            {
                clone = t;
            }
            else
            {
                clone = (T)memberwiseClone(obj);
            }
            return clone;
        }

        public const string MemberwiseCloneName = "MemberwiseClone";
        private static readonly Func<object, object> memberwiseClone
            = (Func<object, object>)(typeof(object).GetMethod(MemberwiseCloneName, BindingFlags.NonPublic | BindingFlags.Instance)?
            .CreateDelegate(typeof(Func<object, object>)) ?? throw new NotImplementedException($"Метод \"{MemberwiseCloneName}\" не реализован."));
    }
}
