using Common.Standard.Interfaces.Model;

namespace Repositories
{
    public class MainModel : IRepositoriesModel
    {
        public IAuthorized Authorized { get; }
        public IRepository<IProduct> Products { get; }
        public IRepository<IPerson> People { get; }
        public IReadOnlyRepository<IPerson> InitPeopleData { get; }
        public IReadOnlyRepository<IProduct> InitProductsData { get; }

        public MainModel(IAuthorized authorized, IRepository<IProduct> products, IRepository<IPerson> people, IReadOnlyRepository<IProduct> productsData, IReadOnlyRepository<IPerson> peopleData)
        {
            Authorized = authorized;
            Products = products;
            People = people;
            InitPeopleData = peopleData;
            InitProductsData = productsData;
        }

        public void InitPeople()
        {
            foreach (var person in InitPeopleData.ToObservableCollections())
            {
                People.Add(person);
            }
        }

        public void InitProducts()
        {
            foreach (var product in InitProductsData.ToObservableCollections())
            {
                Products.Add(product);
            }
        }
    }
}
