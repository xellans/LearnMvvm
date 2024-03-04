namespace Common.Standard.Interfaces.Model
{
    public interface IProduct : IId
    {
        public string Name { get; }
        public string Description { get; }
    }

    public struct ProductDto : IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
    }
}
