using Common.Standard.Interfaces.Model;
using Common.Standard.Interfaces.ViewModel;
using DataBase;
using Repositories;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;
using WpfCore;

namespace LearnMvvm
{
    public partial class App : Application
    {
        private static ContextRepositories contextRepositories {  get; set; }
        private static NavigatorLocator locator { get; set; }
        private static Context context { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            locator = (NavigatorLocator)this.FindResource("locator");
            context = new Context();
            contextRepositories = new ContextRepositories(context);
            IAuthorized authorized = new Authorized();
            locator.AuthVM = new AuthVM(authorized);
        }
        public static RoutedCommand SetCurrentContext = new RoutedCommand();

        static App() => CommandManager.RegisterClassCommandBinding(typeof(UIElement), new CommandBinding(SetCurrentContext, CurrentContextCommand));
        private static void CurrentContextCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (nameof(PersonVM).Equals(e.Parameter))
            {
                if (locator.PersonVM == null)
                {
                    IPersonModel model = new PersonModel(contextRepositories.Person());
                    locator.PersonVM = new PersonVM(model);
                }
                locator.CurrentContext = locator.PersonVM;
            }

            if (nameof(ProductVM).Equals(e.Parameter))
            {
                if (locator.ProductVM == null)
                {
                    IProductModel model = new ProductModel(contextRepositories.Products());
                    locator.ProductVM = new ProductVM(model);
                }
                locator.CurrentContext = locator.ProductVM;
            }

            if (nameof(SettingVM).Equals(e.Parameter))
            {
                if (locator.SettingVM == null)
                    locator.SettingVM = new SettingVM();
                locator.CurrentContext = locator.SettingVM;
            }
        }

    }

}
