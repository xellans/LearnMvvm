using Common.Standard.Interfaces.ViewModel;
using Repositories;
using System.Windows;
using WpfCore;
using Common.Standard.Interfaces.Model;
using DataBase;
using ViewModel;

namespace LearnMvvm
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        //private ContextRepositories ContextRepositories { get; set; }
        #region Регион Локатора
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }

        public IPersonVM? PersonVM { get => Get<IPersonVM>(); set => Set(value); }

        public IProductVM? ProductVM { get => Get<IProductVM>(); set => Set(value); }
        public ISettingVM? SettingVM { get => Get<SettingVM>(); set => Set(value); }
        #endregion

        //public NavigatorLocator()
        //{
        //    Context context = new Context();
        //    ContextRepositories = new ContextRepositories(context);
        //    IAuthorized authorized = new Authorized();
        //    AuthVM = new AuthVM(authorized);
        //}

        #region Регион Навигатора
        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(PersonVM).Equals(obj))
            {
                //IPersonModel model = new PersonModel(ContextRepositories.Person());
                //if(PersonVM == null)
                //PersonVM = new PersonVM(model);
                obj = PersonVM;
            }

            if (nameof(ProductVM).Equals(obj))
            {
                //IProductModel model = new ProductModel(ContextRepositories.Products());
                //if (ProductVM == null)
                //    ProductVM = new ProductVM(model);
                obj = ProductVM;
            }

            if (nameof(SettingVM).Equals(obj))
            {
                //if (SettingVM == null)
                //    SettingVM = new SettingVM();
                obj = SettingVM;
            }

            CurrentContext = obj;
        });

        #endregion

        #region Регион вспомогательного AP-свойства для упрощения привязки в XAML.
        public static NavigatorLocator GetNavigator(DependencyObject obj)
        {
            return (NavigatorLocator)obj.GetValue(NavigatorProperty);
        }

        public static void SetNavigator(DependencyObject obj, NavigatorLocator value)
        {
            obj.SetValue(NavigatorProperty, value);
        }

        // Using a DependencyProperty as the backing store for Navigator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigatorProperty =
            DependencyProperty.RegisterAttached("Navigator", typeof(NavigatorLocator), typeof(NavigatorLocator), new PropertyMetadata(null));
        #endregion
    }
}
