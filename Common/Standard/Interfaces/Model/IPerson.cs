
namespace Common.Standard.Interfaces.Model
{
    public interface IPerson : IId
    {
        public string Name { get; }
        public int CompletedTasks { get;  }
        public int RemainsExecute {  get; }
    }
}
