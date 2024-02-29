using Common.Standard.Interfaces.Model;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace DataBase.Realisation
{
    public class Command<T> : IRepository<T> where T : class
    {
        private readonly Context Context;
        public Command()
        {
            Context = new Context();
            Context.Database.EnsureCreated();
        }
        public void Load() => Context.Set<T>().Load();
        /// <summary>
        /// Возвращает первый найденный элемент или null
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public T? FirstOrDefault(Func<T, bool> Expression) => Context.Set<T>().FirstOrDefault(Expression);

        public T? FirstOrDefault() => Context.Set<T>().FirstOrDefault();

        /// <summary>
        /// Возвращает true, если хотя бы один элемент коллекции соответсвует определенному условию
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>

        public bool Any(Func<T, bool> Expression) => Context.Set<T>().Any(Expression);

        /// <summary>
        ///  Проецирует последовательность
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public IEnumerable<T> Where(Func<T, bool> Expression) => Context.Set<T>().Where(Expression);

        /// <summary>
        /// Добавить новый элемент в бд
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Add(T t)
        {
            Context.Add(t);
            Context.SaveChanges();
            return t;
        }

        public T Clone(T t) => t;

        /// <summary>
        /// Удаление объекта из бд
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public void Remove(int Id)
        {
            var entity = Context.Set<T>().Find(Id);
            if (entity != null)
            {
                Context.Remove(entity);
                Context.SaveChanges();
            }
        }
        public void Remove(T t)
        {
            if (t != null)
            {
                Context.Remove(t);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Обновление данных в бд
        /// </summary>
        /// <param name="NewValue"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void Update(object NewValue, int Id)
        {
            var entity = Context.Set<T>().Find(Id);
            if (entity != null)
            {
                Context.Entry(entity).CurrentValues.SetValues(NewValue);
                Context.SaveChanges();
            }
        }
        public void Update(T t)
        {
            if (t != null)
            {
                Context.Update(t);
                Context.SaveChanges();
            }
        }
        public ReadOnlyObservableCollection<T> ToObservableCollections() => new ReadOnlyObservableCollection<T>(Context.Set<T>().Local.ToObservableCollection());

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void SaveChanges() => Context.SaveChanges();
    }
}
