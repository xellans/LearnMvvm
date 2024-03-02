

namespace Common.Standard.Interfaces.Model
{
    public interface IProduct : IId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
