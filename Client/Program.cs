using Autofac;
using Database;
using Logic;

namespace Client
{
    class Program
    {
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<Dialogue>().As<IDialogue>();
            builder.RegisterType<MixtureManager>().As<IMixtureManager>();
            builder.RegisterType<DataProxy>().As<IDataProxy>();
            builder.RegisterType<MockedDatabaseDataSource>().As<IDataSource>();
            var container = builder.Build();

            using var scope = container.BeginLifetimeScope();
            var app = scope.Resolve<IApplication>();
            app.Run();
        }
    }
}
