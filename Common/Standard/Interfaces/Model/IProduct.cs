

namespace Common.Standard.Interfaces.Model
{
    public interface IProduct : IId
    {
        public string Name { get; }
        public string Description { get;}
    }
}
