using Common.Standard.Interfaces.Model;

namespace Repositories
{
    public class MainModel : IRepositoriesModel
    {
        public IAuthorized Authorized { get; }
        public IRepository<IProduct> Products { get; }
        public IRepository<IPerson> Person { get; }

        public MainModel(IAuthorized authorized, IRepository<IProduct> products, IRepository<IPerson> person)
        {
            Authorized = authorized;
            Products = products;
            Person = person;
        }
    }
}
