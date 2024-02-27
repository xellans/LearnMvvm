using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Inerfaces
{
    public interface IRepository<T>
    {
        void Load();
        T? FirstOrDefault();

        T? FirstOrDefault(Func<T, bool> predicate);
        IEnumerable<T> Where(Func<T, bool> predicate);

        bool Any(Func<T, bool> predicate);
        void Remove(T t);
        void Remove(int Id);
        void Update(T t);
        void Update(object NewValue, int Id);
        T? Clone(T t);
        T? Add(T t);
        ReadOnlyObservableCollection<T> ToObservableCollections();
    }
}
