using Common.Standard.Interfaces.ViewModel;
using System.ComponentModel;
using System.Windows;
using WpfCore;

namespace LearnMvvm
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }

        public NavigatorLocator()
        {
            if (IsInDesignMode)
            {
                AuthVM = new AuthVMDesignMode();
            }
        }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(AuthVM).Equals(obj))
                obj = AuthVM;
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

    // Тестовый класс для режима разработки.
    internal class AuthVMDesignMode : IAuthVM
    {
        public RelayCommand AuthorizeCommand { get; } = new RelayCommand(() => { });
        public long Id { get; set; } = 12344;
        public bool IsAuthorized { get; } = false;
        public string? Name { get; set; } = "Абра Катабра";
    }
}
