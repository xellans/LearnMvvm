using DataBase;
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
using Repositories;
using Repositories.Inerfaces;

namespace ViewModel
{
    public class AuthVM : BaseInpc, IUser
    {
        public AuthVM()
        {
            Instance = this;
            CommandUser = new();
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
        Command<User> CommandUser { get; set; }

        private ICommand _AddUserCommand;
        public ICommand AddUserCommand => _AddUserCommand = _AddUserCommand ?? new RelayCommand(AuthUser);

        public void AuthUser(object obj)
        {
            User.IsAuthorized = true;
            User.Name = Name;
            if (!CommandUser.IsExist(User))
            {
                CommandUser.Add(User);
                NavigationVM.AuthVMClose();
            }
        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        void AutnMethod()
        {
            User = CommandUser.Context.User.FirstOrDefault();
            if (User == null)
                User = new();
            if (User.IsAuthorized)
                NavigationVM.AuthVMClose();
        }

    }
}
