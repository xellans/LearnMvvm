using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using WpfCore;

namespace LearnMvvm.Model.ViewModel
{
    public class PersonVM : ViewModelBase, IPersonVM
    {
        public IReadOnlyObservableCollection<IPerson> PersonList { get; }
        private readonly IRepository<IPerson> people;

        public PersonVM(IRepository<IPerson> people)
        {
            this.people = people;
            PersonList = people.ToObservableCollections();
        }
    }
}
