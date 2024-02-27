using Common.Standard.Interfaces.Model;
using System.Linq.Expressions;

namespace DataBase
{
    public class PeopleRepository : IRepository<IPerson>
    {
       internal readonly Context context;

        public PeopleRepository(Context context)
        {
            this.context = context;
        }

        public IPerson? Add(IPerson t)
        {
            throw new NotImplementedException();
        }

        public bool Any(Func<IPerson, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IPerson? FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public IPerson? FirstOrDefault(Expression<Func<IPerson, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Remove(IPerson t)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyObservableCollection<IPerson> ToObservableCollections()
        {
            throw new NotImplementedException();
        }

        public IPerson Update(IPerson t)
        {
            throw new NotImplementedException();
        }

        public IPerson Update(object NewValue, int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IPerson> Where(Expression<Func<IPerson, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}