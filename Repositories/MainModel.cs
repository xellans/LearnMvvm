using Common.Standard.Interfaces.Model;

namespace Repositories
{
    public class MainModel : IRepositoriesModel
    {
        public IAuthorized Authorized { get; }
        public IRepository<IProduct> Products { get; }
        public IRepository<IPerson> People { get; }

        public MainModel(IAuthorized authorized, IRepository<IProduct> products, IRepository<IPerson> people)
        {
            Authorized = authorized;
            Products = products;
            People = people;
        }
    }
}
