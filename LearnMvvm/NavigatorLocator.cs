using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfCore;

namespace LearnMvvm
{
    // Это классы уровня View
    public class NavigatorLocator : ViewModelBase
    {
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());
        public IAuthVM? AuthVM { get => Get<IAuthVM>(); set => Set(value); }
        public IPersonVM? PersonVM { get => Get<IPersonVM>(); set => Set(value); }
        public IProductVM? ProductVM { get => Get<IProductVM>(); set => Set(value); }

        public NavigatorLocator()
        {
            if (IsInDesignMode)
            {
                AuthVM = new AuthVMDesignMode();
                PersonVM = new PersonVMDesignMode();
                ProductVM = new ProductVMDesignMode();
            }
        }

        public object? CurrentContext { get => Get<object>(); set => Set(value); }

        public RelayCommand SetCurrentContext => GetCommand<object?>(obj =>
        {
            if (nameof(AuthVM).Equals(obj))
                obj = AuthVM;
            else if (nameof(PersonVM).Equals(obj))
                obj = PersonVM;
            else if (nameof(ProductVM).Equals(obj))
                obj = ProductVM;
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
    // Тестовый класс для режима разработки.
    internal class PersonVMDesignMode : IPersonVM
    {
        public IReadOnlyObservableCollection<IPerson> PersonList { get; } = new ReadOnlyObservableList<IPerson, Person>(new ObservableCollection<Person>()
        {
            new Person() {Name = "First"},
            new Person() {Name = "Second"}
        });
        public RelayCommand Refresh { get; } = new RelayCommand(() => { });
    }
    internal class Person : IPerson
    {
        public string Name { get; set; } = string.Empty;
        public int CompletedTasks { get; set; }
        public int RemainsExecute { get; set; }
        public long Id { get; }
    }
    // Тестовый класс для режима разработки.
    internal class ProductVMDesignMode : IProductVM
    {
        public ICommand Add { get; } = new RelayCommand(() => { });
        public ICommand Delete { get; } = new RelayCommand(() => { });
        public IReadOnlyObservableCollection<IProduct> ProductDataList { get; } = new ReadOnlyObservableList<IProduct, Product>(new ObservableCollection<Product>()
        {
            new Product() {Name = "First"},
            new Product() {Name = "Second"}
        });
        public ICommand SaveEdit { get; } = new RelayCommand(() => { });
        public IProduct? Selected { get; set; }
    }

    internal class Product : IProduct
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long Id { get; set; }
    }
}
