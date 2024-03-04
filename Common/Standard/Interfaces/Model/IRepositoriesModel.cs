namespace Common.Standard.Interfaces.Model
{
    public interface IRepositoriesModel
    {
        IAuthorized Authorized { get; }
        IRepository<IPerson> People { get; }
        IRepository<IProduct> Products { get; }

        IReadOnlyRepository<IPerson> InitPeopleData { get; }
        IReadOnlyRepository<IProduct> InitProductsData { get; }

        void InitPeople();
        void InitProducts();
    }
}
