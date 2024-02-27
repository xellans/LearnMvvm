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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NavigatorLocator locator = (NavigatorLocator) FindResource("locator");
            if (!NavigatorLocator.IsInDesignMode)
            {
                // Создание экземпляров и внедрение зависимостей

                Context context = new();

                PeopleRepository peopleRepository = new(context);
                ProdustsRepository produstsRepository = new(context);
                Authorized authorized = new(context);

                MainModel mainModel = new MainModel(authorized, produstsRepository, peopleRepository);

                AuthVM authVM = new(mainModel.Authorized);

                locator.AuthVM = authVM;
            }
        }
    }
}
