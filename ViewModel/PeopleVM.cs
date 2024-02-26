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

namespace ViewModel
{
    public class PeopleVM: ViewModelBase
    {
        public PeopleVM()
        {
            CommandPeople = new();
            PeopleList = new();
            Load();

        }
        #region Заполнение данными
        private void Load()
        {
            var data = CommandPeople.GetPeopleCollection();

            if (data.Count == 0)
                CommandPeople.CreatePeople();

            foreach (var res in CommandPeople.GetPeopleCollection())
                PeopleList.Add(new PeopleData() { Id = res.Id, Name = res.Name, CompletedTasks = res.CompletedTasks, RemainsExecute = res.RemainsExecute });
        }
        #endregion
        public ObservableCollection<PeopleData> PeopleList { get => Get<ObservableCollection<PeopleData>>(); set => Set(value); }
        private Command<People> CommandPeople { get; set; }
    }
    public class PeopleData : ViewModelBase, IPeople
    {
        public long Id { get => Get<long>(); set => Set(value); }

        public string Name { get => Get<string>(); set => Set(value); }

        public int CompletedTasks { get => Get<int>(); set => Set(value); }

        public int RemainsExecute { get => Get<int>(); set => Set(value); }
    }
}
