﻿using DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.VmHellper;
using Repositories;
using Repositories.Inerfaces;

namespace ViewModel
{
    public class PeopleVM: BaseInpc
    {
        public PeopleVM()
        {
            CommandPeople = new();
            PeopleList = new();
            var data = CommandPeople.GetPeopleCollection();

            if (data.Count == 0)
                CommandPeople.CreatePeople();

            foreach (var res in CommandPeople.GetPeopleCollection())
                PeopleList.Add(new PeopleData() { Id = res.Id, Name = res.Name, CompletedTasks = res.CompletedTasks, RemainsExecute = res.RemainsExecute });

        }
        private ObservableCollection<PeopleData> _PeopleList;

        public ObservableCollection<PeopleData> PeopleList { get => _PeopleList; set => Set(ref _PeopleList, value); }
        private Command<People> CommandPeople { get; set; }
    }
    public class PeopleData : BaseInpc, IPeople
    {
        private long _Id;
        public long Id { get => _Id; set => Set(ref _Id, value); }

        private string _Name;
        public string Name { get => _Name; set => Set(ref _Name, value); }

        private int _CompletedTasks;
        public int CompletedTasks { get => _CompletedTasks; set => Set(ref _CompletedTasks, value); }

        private int _RemainsExecute;
        public int RemainsExecute { get => _RemainsExecute; set => Set(ref _RemainsExecute, value); }
    }
}
