using Common.Standard.Interfaces.Model;

namespace Repositories
{
    public class MainModel : IRepositoriesModel
    {
        public IAuthorized Authorized { get; }
        public IRepository<IProduct> Products { get; }
        public IRepository<IPerson> Person { get; }

        public MainModel(IAuthorized Authorized) => this.Authorized = Authorized;
        public MainModel(IRepository<IProduct> Products) => this.Products = Products;
        public MainModel(IRepository<IPerson> Person) => this.Person = Person;
    }
}
