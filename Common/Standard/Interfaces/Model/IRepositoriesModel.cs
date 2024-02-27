namespace Common.Standard.Interfaces.Model
{
    public interface IRepositoriesModel
    {
        IAuthorized Authorized { get; }
        IRepository<IPerson> People { get; }
        IRepository<IProduct> Products { get; }
    }
}
