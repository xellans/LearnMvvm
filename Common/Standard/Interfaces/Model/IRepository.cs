using System.Linq.Expressions;

namespace Common.Standard.Interfaces.Model
{
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        void Remove(T t);
        void Remove(int Id);
        T Update(object NewValue, int Id);
        T Update(T t);
        T? Add(T t);
    }
}
