using System.Linq.Expressions;

namespace Common.Standard.Interfaces.Model
{
    public interface IReadOnlyRepository<T>
    {
        bool Any(Expression<Func<T, bool>> expression);
        T? FirstOrDefault();
        T? FirstOrDefault(Expression<Func<T, bool>> expression);
        void Load();
        IReadOnlyObservableCollection<T> ToObservableCollections();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}