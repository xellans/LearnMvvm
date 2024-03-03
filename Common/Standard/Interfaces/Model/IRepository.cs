using Microsoft.EntityFrameworkCore;
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
        T? Add(T t);

        //Смысла нет в этом методе. Для его использования нужно, что T был с мутабльными свойствами.
        //А смысл нашей реализации в том, чтобы T мог менять только Репозиторий.
        //T? NewT();
        
        IReadOnlyObservableCollection<T> ToObservableCollections();
    }
}
