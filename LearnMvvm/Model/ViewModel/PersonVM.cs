using WpfCore;
using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using Repositories;

namespace LearnMvvm.Model.ViewModel
{
    public class PersonVM: ViewModelBase, IPersonVM
    {
        public PersonVM()
        {
            Repository = new PersonModel();
            PersonList = Repository.PersonCollections;
        }
        public IReadOnlyObservableCollection<IPerson> PersonList { get => Get<IReadOnlyObservableCollection<IPerson>>(); set => Set(value); }
        public PersonModel Repository;
    }
}
