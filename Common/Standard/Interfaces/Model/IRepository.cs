using System.Linq.Expressions;

namespace Common.Standard.Interfaces.Model
{
    public interface IRepository<T>
    {
        void Load();
        T? FirstOrDefault();

        T? FirstOrDefault(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        bool Any(Expression<Func<T, bool>> expression);
        void Remove(T t);
        void Remove(int Id);
        T Update(T t);
        T Update(object NewValue, int Id);
        T? Add(T t);
        IReadOnlyObservableCollection<T> ToObservableCollections();
    }
}
