using DataBase.Command;
using DataBase.Entity;
using InterfaceList;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel;
using ViewModel.VmHellper;

namespace ViewModel
{
    public class AuthVM : BaseInpc, IUser
    {
        public AuthVM()
        {
            Instance = this;
            CommandUser = new CommandUser();
            AutnMethod();
        }
        public long _Id;
        public long Id { get => _Id; set => Set(ref _Id, value); }

        public string _Name;
        public string Name { get => _Name; set => Set(ref _Name, value); }

        public bool _IsAuthorized;
        public bool IsAuthorized { get => _IsAuthorized; set => Set(ref _IsAuthorized, value); }

        public static AuthVM Instance { get; set; } //Делаем Vm статической для доступа в рамках всего проекта

        public User User { get; set; }
        CommandUser CommandUser { get; set; }

        private ICommand _AddUserCommand;
        public ICommand AddUserCommand => _AddUserCommand = _AddUserCommand ?? new RelayCommand(AuthUser);

        public void AuthUser(object obj)
        {
            User.IsAuthorized = true;
            User.Name = Name;
            if (!CommandUser.IsExistUser(User))
            {
                CommandUser.AddUser(User);
                NavigationVM.AuthVMClose();
            }
        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        void AutnMethod()
        {
            User = CommandUser.GetUser();
            if (User == null)
                User = new();
            if (User.IsAuthorized)
                NavigationVM.AuthVMClose();
        }

    }
}
