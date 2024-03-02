using Common.Standard.Interfaces.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase
{
    [Table("People")]
    public class Person : IPerson
    {
        private string _name = string.Empty;

        public long Id { get; set; }
        public string Name { get => _name; set => _name = value ?? string.Empty; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
    }
}
