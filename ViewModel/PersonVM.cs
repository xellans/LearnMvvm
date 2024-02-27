using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Repositories.Inerfaces;
using WpfCore;
using Repositories.Realisation;

namespace ViewModel
{
    public class PersonVM: ViewModelBase
    {
        public PersonVM()
        {
            Person = new();
            PersonList = new();
            Load();

        }
        #region Заполнение данными
        private void Load()
        {
            foreach (var res in Person.PersonCollections)
                PersonList.Add(new PersonData() { Id = res.Id, Name = res.Name, CompletedTasks = res.CompletedTasks, RemainsExecute = res.RemainsExecute });
        }
        #endregion
        public ObservableCollection<PersonData> PersonList { get => Get<ObservableCollection<PersonData>>(); set => Set(value); }
        private  PersonRepository Person;
    }
    public class PersonData : ViewModelBase, IPerson
    {
        public long Id { get => Get<long>(); set => Set(value); }

        public string Name { get => Get<string>(); set => Set(value); }

        public int CompletedTasks { get => Get<int>(); set => Set(value); }

        public int RemainsExecute { get => Get<int>(); set => Set(value); }
    }
}
