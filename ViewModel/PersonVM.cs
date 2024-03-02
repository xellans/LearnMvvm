using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using WpfCore;

namespace ViewModel
{
    public class PersonVM : ViewModelBase, IPersonVM
    {
        public PersonVM(IRepository<IPerson> people)
        {
            //Person = new();
            this.people = people;

            PersonList = people.ToObservableCollections();
            people.Load();

        }
        //#region Заполнение данными
        //private void Load()
        //{
        //    foreach (var res in Person.PersonCollections)
        //        PersonList.Add(new PersonData() { Id = res.Id, Name = res.Name, CompletedTasks = res.CompletedTasks, RemainsExecute = res.RemainsExecute });
        //}
        //#endregion

        public RelayCommand Refresh => GetCommand(people.Load);
        public IReadOnlyObservableCollection<IPerson> PersonList { get; }
        private readonly IRepository<IPerson> people;
    }
    //public class PersonData : ViewModelBase, IPerson
    //{
    //    public long Id { get => Get<long>(); set => Set(value); }

    //    public string Name { get => Get<string>() ?? string.Empty; set => Set(value); }

    //    public int CompletedTasks { get => Get<int>(); set => Set(value); }

    //    public int RemainsExecute { get => Get<int>(); set => Set(value); }
    //}
}
