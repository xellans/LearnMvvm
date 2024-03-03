using WpfCore;
using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;

namespace ViewModel
{
    public class PersonVM: ViewModelBase, IPersonVM
    {
        public PersonVM(IPersonModel Person)
        {
            this.Person = Person;
            PersonList = Person.PersonCollections;
        }
        public IReadOnlyObservableCollection<IPerson> PersonList { get => Get<IReadOnlyObservableCollection<IPerson>>(); set => Set(value); }
        public IPersonModel Person { get; set; }
    }
}
