using Common.Standard.Interfaces.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Common.EntityFrameworkCore
{
    public class Repository<TTarget, TSource> : IRepository<TTarget>
        where TTarget : IId
        where TSource : class, TTarget
    {
        internal readonly DbContext context;
        internal readonly Func<TTarget, DbContext, TSource> itToT;
        public DbSet<TSource> set { get; set; }

        internal readonly Exception notId;
        internal readonly Func<TTarget, TSource, bool> equalsValues;
        internal readonly Exception noEqualsValues;
        internal readonly Func<TTarget, TSource> convert;

        /// <summary>Создание экземпляра репозитория.</summary>
        /// <param name="context"><see cref="DbContext"/> из которого можно извлечь
        /// <see cref="DbSet{TEntity}"/> для <see cref="TSource"/>.</param>
        /// <param name="itToT">Делегат метода извлекающего из экземпляра типа
        /// <see cref="TTarget"/>, или создающего из него, экземпляр
        /// <see cref="TSource"/> для добавления в БД.</param>
        /// <param name="notId">Исключение выкидваемое в случае отсутвия в БД
        /// экземпляра <see cref="TSource"/> с указанным Id.</param>
        /// <param name="equalsValues">Делегат метода проверяющего
        /// на эквивалентность значений экземпляры <see cref="TTarget"/>
        /// и <see cref="TSource"/>.</param>
        /// <param name="noEqualsValues">сключение выкидваемое в случае
        /// неэквивалентности значений экземпляров <see cref="TTarget"/>
        /// и <see cref="TSource"/>.</param>
        /// <param name="convert">Делегат метода извлекающего из экземпляра
        /// типа <see cref="TTarget"/>, или создающего из него,
        /// экземпляр <see cref="TSource"/> для обновления в БД.</param>
        public Repository(DbContext context,
                          Func<TTarget, DbContext, TSource> itToT,
                          Exception notId,
                          Func<TTarget, TSource, bool> equalsValues,
                          Exception noEqualsValues,
                          Func<TTarget, TSource> convert)
        {
            this.context = context;
            this.itToT = itToT;
            set = context.Set<TSource>();
            this.notId = notId;
            this.equalsValues = equalsValues;
            this.noEqualsValues = noEqualsValues;
            this.convert = convert;
        }

        public TTarget? Add(TTarget it)
        {
            TSource t = itToT(it, context);
            t.Id = 0;
            var result = set.Add(t);
            context.SaveChanges();
            return result.Entity;
        }
        public TTarget? NewT() => Activator.CreateInstance<TSource>();

        public bool Any(Expression<Func<TTarget, bool>> expression)
        {
            return set.Any(expression);
        }

        public TTarget? FirstOrDefault()
        {
            return set.FirstOrDefault();
        }

        public TTarget? FirstOrDefault(Expression<Func<TTarget, bool>> expression)
        {
            return set.FirstOrDefault(expression);
        }

        public void Load()
        {
            set.Load();
        }

        public void Remove(TTarget it)
        {
            var t = set.Find(it.Id) ?? throw notId;
            if (!equalsValues(it, t))
            {
                throw noEqualsValues;
            }
            set.Remove(t);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var t = set.Find(id) ?? throw notId;
            set.Remove(t);
            context.SaveChanges();
        }
        public IReadOnlyObservableCollection<TTarget> ToObservableCollections()
        {
            set.Load();
            IReadOnlyObservableCollection<TTarget>? collection;
            var list = set.Local.ToObservableCollection();
            collection = new ReadOnlyObservableList<TTarget, TSource>(list);
            return collection;
        }

        public TTarget Update(TTarget it)
        {
            var t = set.Find(it.Id) ?? throw notId;
            var entity = context.Entry(t);
            entity.CurrentValues.SetValues(it);
            context.SaveChanges();
            return it;
        }
        public IQueryable<TTarget> Where(Expression<Func<TTarget, bool>> expression)
        {
            return set.Where(expression);
        }

    }
}