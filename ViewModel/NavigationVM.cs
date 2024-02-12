using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.VmHellper;
using Repositories;

namespace ViewModel
{
    public class NavigationVM: BaseInpc
    {
        public NavigationVM()
        {
            ProductVMCommand = new RelayCommand(ActionProductVM);
            PeopleVMCommand = new RelayCommand(ActionPeopleVM);
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
        /// <summary>
        /// ListCurrentView нужен для хранения состояния UserControl. 
        /// Все действия над представлением и изменением контента сохраняются в этой коллекции. 
        /// Если хранить  представление и изменением контента не нужно. Тогда лучше удалить эту коллекцию и связанный с ней метод.
        /// </summary>
        private List<object> ListCurrentView = new();
        private object GetOrAddObject(object obj)
        {
           var control = ListCurrentView.FirstOrDefault(obj);
            if (control == null)
            {
                control = obj;
                ListCurrentView.Add(obj);
            }
            return control;
        }

        #region Экземляер для отображения всплывающих окон
        private object _AppearingUserControl;
        public object AppearingUserControl { get => _AppearingUserControl; set { Set(ref _AppearingUserControl, value); } }
        #endregion

        #region Экземляер для отображения страниц меню
        private object _CurrentMenu;
        public object CurrentMenu { get => _CurrentMenu; set { Set(ref _CurrentMenu, value); } }
        #endregion

        //# region AuthVMCommand
        //private ICommand _AuthVM;
        //public ICommand AuthVMCommand => _AuthVM = _AuthVM ?? new RelayCommand((object obj) => { AppearingUserControl = new AuthVM(); });
        //#endregion

        #region PeopleVMCommand
        private void ActionPeopleVM(object obj) => CurrentMenu = GetOrAddObject(new PeopleVM());
        public ICommand PeopleVMCommand { get; set; }
        #endregion

        #region PeopleVMCommand
        private void ActionProductVM(object obj) => CurrentMenu = GetOrAddObject(new ProductVM());
        public ICommand ProductVMCommand { get; set; }
        #endregion


    }
}
