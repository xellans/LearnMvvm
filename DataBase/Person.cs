using Common.Standard.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Person : IPerson
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public  virtual int CompletedTasks { get; set; }
        public virtual int RemainsExecute { get; set; }
    }
}
