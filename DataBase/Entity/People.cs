using InterfaceList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entity
{
    public class People: IPeople
    {
       public long Id { get; set; }
       public string Name { get; set; }
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
    }
}
