using DataBase;
using LearnMvvm;
using Repositories;
using System.CodeDom.Compiler;
using System.Diagnostics;
using ViewModel;

namespace LearnMvvmApp
{
    class Program
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread()]
        [DebuggerNonUserCode()]
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        static void Main(string[] args)
        {
            App app = new App();
            app.Startup += delegate
            {
                NavigatorLocator locator = (NavigatorLocator)app.FindResource("locator");
                if (!NavigatorLocator.IsInDesignMode)
                {
                    // Создание экземпляров и внедрение зависимостей

                    Context context = new();
                    ContextRepositories repositories = new(context);


                    //Authorized authorized = new(context);
                    Authorized.Implementation authorized = new("users.json");

                    MainModel mainModel = new MainModel(authorized, repositories.Products, repositories.People);

                    AuthVM authVM = new(mainModel.Authorized);

                    locator.AuthVM = authVM;
                }
            };
            app.InitializeComponent();
            app.Run();
        }
    }
}
