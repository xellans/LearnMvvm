namespace Common.Standard.Interfaces.Model
{
    public interface IPerson : IId
    {
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
    }
}
