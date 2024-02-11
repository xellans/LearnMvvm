using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceList
{
    public interface IPeople
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute {  get; set; }
    }
}
