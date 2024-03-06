using Common.Standard.Interfaces.ViewModel;
using Repositories;
using System.Windows;
using WpfCore;
using Common.Standard.Interfaces.Model;
using DataBase;
using ViewModel;

namespace LearnMvvm
{
    public class NavigatorLocator : ViewModelBase
    {
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }
        
        public IPersonVM? PersonVM { get => Get<IPersonVM>(); set => Set(value); }

        public IProductVM? ProductVM { get => Get<IProductVM>(); set => Set(value); }
        public ISettingVM? SettingVM { get => Get<SettingVM>(); set => Set(value); }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }



        public static NavigatorLocator GetNavigator(DependencyObject obj) => (NavigatorLocator)obj.GetValue(NavigatorProperty);

        public static void SetNavigator(DependencyObject obj, NavigatorLocator value) => obj.SetValue(NavigatorProperty, value);

        public static readonly DependencyProperty NavigatorProperty =
            DependencyProperty.RegisterAttached("Navigator", typeof(NavigatorLocator), typeof(NavigatorLocator), new PropertyMetadata(null));


    }
}
