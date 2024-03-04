
namespace Common.Standard.Interfaces.Model
{
    public interface IPerson : IId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute {  get; set; }
    }
}
