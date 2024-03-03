using Common.Standard.Interfaces.Model;

namespace DataBase
{
    public class Person : IPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
    }
}
