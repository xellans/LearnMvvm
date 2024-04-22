using Common.Standard.Interfaces.ViewModel;
using DataBase;
using Repositories;
using System.Windows;
using ViewModel;

namespace LearnMvvm
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NavigatorLocator locator = (NavigatorLocator)this.FindResource("locator");
            Context context = new Context();
            ContextRepositories contextRepositories = new ContextRepositories(context);
            InstancesProvider.Register<IAuthVM>(() => new AuthVM(new Authorized(context)));
            InstancesProvider.Register<IProductVM>(() => new ProductVM(new ProductModel(contextRepositories.Products())));
            InstancesProvider.Register<IPersonVM>(() => new PersonVM(new PersonModel(contextRepositories.Person())));
            InstancesProvider.Register<ISettingVM>(() => new SettingVM());
        }
    }
}
