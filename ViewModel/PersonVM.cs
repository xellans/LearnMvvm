using Common.Standard.Interfaces.Model;
using System.Collections.ObjectModel;
using WpfCore;

namespace ViewModel
{
    public class PersonVM : ViewModelBase
    {
        public PersonVM()
        {
            //Person = new();
            PersonList = new();
            Load();

        }
        #region Заполнение данными
        private void Load()
        {
            //foreach (var res in Person.PersonCollections)
            //    PersonList.Add(new PersonData() { Id = res.Id, Name = res.Name, CompletedTasks = res.CompletedTasks, RemainsExecute = res.RemainsExecute });
        }
        #endregion
        public ObservableCollection<PersonData>? PersonList { get => Get<ObservableCollection<PersonData>>(); set => Set(value); }
        private readonly IRepository<IPerson> people;
    }
    public class PersonData : ViewModelBase, IPerson
    {
        public long Id { get => Get<long>(); set => Set(value); }

        public string Name { get => Get<string>() ?? string.Empty; set => Set(value); }

        public int CompletedTasks { get => Get<int>(); set => Set(value); }

        public int RemainsExecute { get => Get<int>(); set => Set(value); }
    }
}
