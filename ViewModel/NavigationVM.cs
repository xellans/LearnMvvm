using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Repositories;
using System.Windows;
using WpfCore;
using Common.Standard.Interfaces.Model;

namespace ViewModel
{
    public class NavigationVM: ViewModelBase
    {
        private readonly Authorized Auth;
        public NavigationVM()
        {
            CommandUser = new();
            User? user = CommandUser.Context.User.FirstOrDefault() ?? new User();
            Auth = new Authorized();
            Auth.AuthorizedChanged += Authorized_AuthorizedChanged;
            Auth.Authorize(user.Name);

            if(Auth.IsAuthorized)
                CurrentMenu = new PersonVM();
            else
            AuthVM.Instance.AuthorizeCommand = _AuthorizeCommand;
        }
        #region Авторизация пользователя
        private void Authorized_AuthorizedChanged(object sender, IsAuthorizedChangedEventArgs e)
        {
            if (AppearingUserControl != null)
                AppearingUserControl = null!;

            if (e.IsAuthorized)
                CurrentMenu = new PersonVM();
            else
                AppearingUserControl = new AuthVM();
        }

        // Команда для выполнения действия, требующего авторизации
        private ICommand authorizeCommand;

        public ICommand _AuthorizeCommand => authorizeCommand ??  new RelayCommand(AuthorizeExecute);

        private void AuthorizeExecute(object name) => Auth.Authorize(name.ToString());
        #endregion

        Command<User> CommandUser;

        #region Экземляер для отображения всплывающих окон
        public object AppearingUserControl { get => Get<object>(); set { Set(value); } }
        #endregion

        #region Экземляер для отображения страниц меню
        public object CurrentMenu { get => Get<object>(); set { Set(value); } }
        #endregion

        #region PeopleVMCommand
        private ICommand _PersonVMCommand;
        public ICommand PersonVMCommand => _PersonVMCommand = _PersonVMCommand ?? new RelayCommand(() => { CurrentMenu = new PersonVM(); });
        #endregion

        #region ProductVMCommand
        private ICommand _ProductVMCommand;
        public ICommand ProductVMCommand  => _ProductVMCommand = _ProductVMCommand ?? new RelayCommand(() => { CurrentMenu = new ProductVM(); });
        #endregion

        #region SettingVMCommand
        private ICommand _SettingVMCommand;
        public ICommand SettingVMCommand => _SettingVMCommand = _SettingVMCommand ?? new RelayCommand(() => { CurrentMenu = new SettingVM(); });
        #endregion


    }
}
