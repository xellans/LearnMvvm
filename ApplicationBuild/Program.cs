﻿using DataBase;
using LearnMvvm;
using LearnMvvm.Model.ViewModel;
using Repositories;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace ApplicationBuild
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
                    context.Database.EnsureCreated();
                    ContextRepositories repositories = new(context);


                    Authorized authorized = new(context);


                    MainModel mainModel = new MainModel(authorized, repositories.Products, repositories.People);

                    AuthVM authVM = new(mainModel.Authorized);

                    locator.AuthVM = authVM;

                    PersonVM personVM = new PersonVM(repositories.People);
                    locator.PersonVM = personVM;

                    ProductVM productVM = new ProductVM(repositories.Products);
                    locator.ProductVM = productVM;
                }
            };
            app.InitializeComponent();
            app.Run();
        }
    }
}