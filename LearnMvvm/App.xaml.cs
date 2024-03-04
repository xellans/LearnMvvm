using Common.Standard.Interfaces.Model;
using DataBase;
using Repositories;
using System.Configuration;
using System.Data;
using System.Windows;
using ViewModel;

namespace LearnMvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ContextRepositories contextRepositories = null!;
        private NavigatorLocator locator = null!;

        // Иницилизация и создание зависимостей.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            locator = (NavigatorLocator)FindResource("locator");

            Context context = new Context();
            context.Database.EnsureCreated();

            contextRepositories = new ContextRepositories(context);
            IAuthorized authorized = new Authorized(context);
            locator.AuthVM = new AuthVM(authorized);
            {
                IPersonModel model = new PersonModel(contextRepositories.Person());
                locator.PersonVM = new PersonVM(model);
            }
            {
                IProductModel model = new ProductModel(contextRepositories.Products());
                locator.ProductVM = new ProductVM(model);
            }
            locator.SettingVM = new SettingVM();
        }
    }

}
