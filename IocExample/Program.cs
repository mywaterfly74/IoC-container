using IocExample.Classes;
using Ninject;

namespace IocExample
{
    class Program
    {
//        static void Main(string[] args)
//        {
//            var logger = new ConsoleLogger();
//            var sqlConnectionFactory = new SqlConnectionFactory("SQL Connection", logger);
//            var createUserHandler = new CreateUserHandler(
//                new UserService(
//                    new QueryExecutor(sqlConnectionFactory), 
//                    new CommandExecutor(sqlConnectionFactory), 
//                    new CacheService(logger, 
//                        new RestClient("API KEY"))), logger);
//
//            createUserHandler.Handle();
//        }

//        static void Main(string[] args)
//        {
//            var kernel = new StandardKernel();
//
//            kernel.Bind<ILogger>().To<ConsoleLogger>();
//            kernel.Bind<IConnectionFactory>()
//                .ToConstructor(key => new SqlConnectionFactory("SQL Connection", key.Inject<ILogger>()))
//                .InSingletonScope();
//            kernel.Bind<CreateUserHandler>().To<CreateUserHandler>();
//            kernel.Bind<UserService>().To<UserService>();
//            kernel.Bind<QueryExecutor>().To<QueryExecutor>();
//            kernel.Bind<CommandExecutor>().To<CommandExecutor>();
//            kernel.Bind<CacheService>().To<CacheService>();
//            kernel.Bind<RestClient>().ToConstructor(key => new RestClient("API_KEY"));
//            kernel.Get<CreateUserHandler>().Handle();
//        }

        static void Main(string[] args)
        {
            DependencyResolver resolver = new DependencyResolver();
        
            resolver.Bind<ILogger, ConsoleLogger>();
            resolver.Bind<IConnectionFactory, SqlConnectionFactory>(
                new SqlConnectionFactory("SQL Connection", resolver.Get<ILogger>()));
            resolver.Bind<CreateUserHandler, CreateUserHandler>();            resolver.Bind<UserService, UserService>();
            resolver.Bind<QueryExecutor, QueryExecutor>();
            resolver.Bind<CommandExecutor, CommandExecutor>();
            resolver.Bind<CacheService, CacheService>();
            resolver.Bind<RestClient, RestClient>(new RestClient("API_KEY"));

            resolver.Get<CreateUserHandler>().Handle();
        }
    }
}
