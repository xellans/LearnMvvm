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
using Repositories;
using Repositories.Inerfaces;
using WpfCore;

namespace ViewModel
{
    public class AuthVM : ViewModelBase
    {
        public AuthVM()
        {
            Instance = this;
            CommandUser = new();
            AutnMethod();
        }
        public long Id { get => Get<long>(); set => Set(value); }

        public string Name { get => Get<string>(); set => Set(value); }

        public bool IsAuthorized { get => Get<bool>(); set => Set(value); }

        public static AuthVM Instance { get; set; } //Делаем Vm статической для доступа в рамках всего проекта

        public User User { get; set; }
        Command<User> CommandUser { get; set; }

        private ICommand _AddUserCommand;
        public ICommand AddUserCommand => _AddUserCommand = _AddUserCommand ?? new RelayCommand(AuthUser);

        public void AuthUser()
        {
            User.IsAuthorized = true;
            User.Name = Name;
            if (!CommandUser.Any(x => x.Name == User.Name))
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
