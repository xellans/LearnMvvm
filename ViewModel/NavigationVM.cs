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

namespace ViewModel
{
    public class NavigationVM: ViewModelBase
    {
        public NavigationVM()
        {
            AuthVMClose = AuthVMCloseRun;
            CommandUser = new();
            User? user = CommandUser.Context.User.FirstOrDefault();
            if (user == null)
            {
                AppearingUserControl = new AuthVM();
                CommandUser = null!;
            }
            else
                CurrentMenu = new PeopleVM();
        }

        Command<User> CommandUser;
        /// <summary>
        /// Делегат для закрытия AppearingUserControl, если понадобится в коде закрыть всплывающее окно.
        /// </summary>
        public static Action AuthVMClose = null!;
        void AuthVMCloseRun() => AppearingUserControl = null!;

        #region Экземляер для отображения всплывающих окон
        public object AppearingUserControl { get => Get<object>(); set { Set(value); } }
        #endregion

        #region Экземляер для отображения страниц меню
        public object CurrentMenu { get => Get<object>(); set { Set(value); } }
        #endregion

        #region PeopleVMCommand
        private ICommand _PeopleVMCommand;
        public ICommand PeopleVMCommand => _PeopleVMCommand = _PeopleVMCommand ?? new RelayCommand(() => { CurrentMenu = new PeopleVM(); });
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
