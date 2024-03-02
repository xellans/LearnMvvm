namespace Common.Standard.Interfaces.Model
{
    public interface IRepositoriesModel
    {
        IAuthorized Authorized { get; }
        IRepository<IPerson> Person { get; }
        IRepository<IProduct> Products { get; }
    }
}
