using DataBase.Realisation;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataBase.Interfaces
{
    public interface IPersonRepository
    {
         Command<Person> Command { get; set; }
         ReadOnlyObservableCollection<Person> PersonCollections {  get; }
         void CreatePerson();
    }
}
