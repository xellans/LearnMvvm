﻿using Common.Standard.Interfaces.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.EntityFrameworkCore
{
    public class Repository<IT, T> : IRepository<IT>
        where IT : IId
        where T : class, IT
    {
        internal readonly DbContext context;
        internal readonly Func<IT, T> itToT;
        internal readonly DbSet<T> set;
        internal readonly Exception notId;
        internal readonly Func<IT, T, bool> equalsValues;
        internal readonly Exception noEqualsValues;
      //  internal readonly Func<IT, T> convert;

        public Repository(DbContext context, Func<IT, T> itToT, Exception notId, Func<IT, T, bool> equalsValues, Exception noEqualsValues)
        {
            this.context = context;
            this.itToT = itToT;
            set = context.Set<T>();
            this.notId = notId;
            this.equalsValues = equalsValues;
            this.noEqualsValues = noEqualsValues;
        }

        public IT? Add(IT it)
        {
            T t = itToT(it);
            t.Id = 0;
            var result = set.Add(t);
            context.SaveChanges();
            return result.Entity;
        }

        public bool Any(Expression<Func<IT, bool>> expression)
        {
            return set.Any(expression);
        }

        public IT? FirstOrDefault()
        {
            return set.FirstOrDefault();
        }

        public IT? FirstOrDefault(Expression<Func<IT, bool>> expression)
        {
            return set.FirstOrDefault(expression);
        }

        public void Load()
        {
            set.Load();
        }

        public void Remove(IT it)
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
        public IReadOnlyObservableCollection<IT> ToObservableCollections()
        {
            set.Load();
            IReadOnlyObservableCollection<IT>? collection;
            var list = set.Local.ToObservableCollection();
            collection = new ReadOnlyObservableList<IT, T>(list);
            return collection;
        }

        public IT Update(IT it)
        {
            var t = set.Find(it.Id) ?? throw notId;
            var entity = context.Entry(t);
            entity.CurrentValues.SetValues(it);
            context.SaveChanges();
            return it;
        }

        public IT Update(object NewValue, int id)
        {
            var t = set.Find(id) ?? throw notId;

            var entity = context.Entry(t);
            entity.CurrentValues.SetValues(NewValue);
            context.SaveChanges();

            return entity.Entity;
        }

        public IQueryable<IT> Where(Expression<Func<IT, bool>> expression)
        {
            return set.Where(expression);
        }

    }
}