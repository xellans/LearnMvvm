using Common.Standard.Interfaces.ViewModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using ViewModel;
using WpfCore;

namespace LearnMvvm
{
    public class NavigatorLocator : ViewModelBase
    {
        public IAuthVM? AuthVM { get => GetOrCreate<IAuthVM>(); set => Set(value); }
        
        public IPersonVM? PersonVM { get => GetOrCreate<IPersonVM>(); set => Set(value); }

        public IProductVM? ProductVM { get => GetOrCreate<IProductVM>(); set => Set(value); }
        public ISettingVM? SettingVM { get => GetOrCreate<ISettingVM>(); set => Set(value); }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        private T? GetOrCreate<T>([CallerMemberName] string propertyName = "") where T : class
        {
            T? value = Get<T>(propertyName);
            if (value is null)
            {
                value = InstancesProvider.GetInstance<T>();
                Set(value, propertyName);
            }
            return value;
        }

        public static readonly RoutedUICommand SetCurrentContext = new RoutedUICommand(
            "Задание текущего контекста навигатору, находящемуся в ресурсах, вызвашего команду, элемента.",
            "SetCurrentContext",
            typeof(NavigatorLocator));

        /// <summary>По умолчанию вызов команды будет происходить только на уроня Окна.</summary>
        static NavigatorLocator()
            => CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(SetCurrentContext, CurrentContextCommand));

        private void CurrentContextExecute(string name)
        {

            if (nameof(PersonVM).Equals(name))
            {
                CurrentContext = PersonVM;
            }

            if (nameof(ProductVM).Equals(name))
            {
                CurrentContext = ProductVM;
            }

            if (nameof(SettingVM).Equals(name))
            {
                CurrentContext = SettingVM;
            }
        }

        private static void CurrentContextCommand(object sender, ExecutedRoutedEventArgs e)
        {
            // Элемент вызвавщий команду
            FrameworkElement element = (FrameworkElement)sender;
            // Получение локатора-навигатора из ресурсов элемента.
            NavigatorLocator locator = (NavigatorLocator)element.FindResource("locator");
            // Вызов метода навигатора переключающего контекст.
            locator.CurrentContextExecute(e.Parameter.ToString() ?? string.Empty);
        }

        public static NavigatorLocator GetNavigator(DependencyObject obj) => (NavigatorLocator)obj.GetValue(NavigatorProperty);

        public static void SetNavigator(DependencyObject obj, NavigatorLocator value) => obj.SetValue(NavigatorProperty, value);

        public static readonly DependencyProperty NavigatorProperty =
            DependencyProperty.RegisterAttached("Navigator", typeof(NavigatorLocator), typeof(NavigatorLocator), new PropertyMetadata(null));


    }

    [MarkupExtensionReturnType(typeof(RoutedUICommand))]
    public class SetCurrentContext : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
           return  NavigatorLocator.SetCurrentContext;
        }
    }
}
