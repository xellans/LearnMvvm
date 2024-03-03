
namespace Common.Standard.Interfaces.Model
{
    public interface IPerson : IId
    {
        //public long Id { get; set; }
        public string Name { get;/* set;*/ }
        public int CompletedTasks { get;/* set;*/ }
        public int RemainsExecute { get; /* set;*/}
    }

    public struct PersonDto : IPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }

        public PersonDto(IPerson person)
        {
            Id = person.Id;
            Name = person.Name;
            CompletedTasks = person.CompletedTasks;
            RemainsExecute = person.RemainsExecute;
        }
    }
}
