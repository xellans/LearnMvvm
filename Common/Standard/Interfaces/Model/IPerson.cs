namespace Common.Standard.Interfaces.Model
{
    public interface IPerson : IId
    {
        public string Name { get; }
        public int CompletedTasks { get;  }
        public int RemainsExecute { get; }
    }

    public struct PersonDto : IPerson
    {
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get;  set; }
        public long Id { get; set; }
    }
}
