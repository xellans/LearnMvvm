using DataBase.Command;
using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel;
using ViewModel.VmHellper;

namespace LearnMvvm
{
    public class  AuthVM : IUser
    {
        public static AuthVM Instance { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        CommandUser CommandUser { get; set; }

        public AuthVM()
        {
            Instance = this;
            AddUserCommand = new RelayCommand(AuthUser);
            CommandUser = new CommandUser();
            User = CommandUser.GetUser();
            if (User == null)
                User = new();
            if(User.IsAuthorized)
            {
                AuthUserControlView = null!;
            }
            else
                AuthUserControlView = new AuthUserControl();
        }
        private User? _user;
        public User? User
        { 
            get => _user;
            set 
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            } 
        }

        public ICommand AddUserCommand { get; set; }
        public void AuthUser(object obj)
        {
            User.IsAuthorized = true; //Не изменяется на true
            if(!CommandUser.IsExistUser(User))
            CommandUser.AddUser(User);
            AuthUserControlView = null!;
        }

        private AuthUserControl _authUserControl;
        public AuthUserControl AuthUserControlView
        {
            get => _authUserControl;
            set 
            { 
                _authUserControl = value;
                OnPropertyChanged(nameof(AuthUserControlView));
            }
        }
    }
}
