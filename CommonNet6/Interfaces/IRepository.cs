
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Interfaces
{
    public interface IRepository<T>
    {
        ReadOnlyObservableCollection<T> GetCollection();
        bool Delete(long id);
        T Clone(T t);
        T? Add(T t);
        T? Update(T t);

    }
}
