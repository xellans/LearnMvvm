using Common.Standard.Interfaces.ViewModel;
using Repositories;
using System.ComponentModel;
using System.Windows;
using WpfCore;
using Common.Standard.Interfaces.Model;

namespace LearnMvvm.Model.ViewModel
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }
        
        public IPersonVM? PersonVM { get => Get<IPersonVM>(); set => Set(value); }

        public IProductVM? ProductVM { get => Get<IProductVM>(); set => Set(value); }


        public NavigatorLocator()
        {
            IAuthorized authorized = new Authorized();
            AuthVM = new AuthVM(authorized);
        }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(PersonVM).Equals(obj))
            {
                PersonVM = new PersonVM();
                obj = PersonVM;
            }

            if (nameof(ProductVM).Equals(obj))
            {
                ProductVM = new ProductVM();
                obj = ProductVM;
            }

            CurrentContext = obj;
        });



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


    }
}
