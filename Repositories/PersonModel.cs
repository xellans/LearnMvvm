using Common.WpfCore;
using DataBase.Interfaces;
using DataBase.Realisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PersonModel
    {
        public IPersonRepository Person { get; set; }
        public PersonModel() 
        {
            Person = new PersonRepository();
        }
    }
}
