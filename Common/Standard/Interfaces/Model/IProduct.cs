namespace Common.Standard.Interfaces.Model
{
    public interface IProduct : IId
    {
        //public long Id { get; set; }
        public string Name { get; /*set; */}
        public string Description { get; /*set;*/ }
    }

    public struct ProductDto : IProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProductDto(IProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
        }
    }
}
