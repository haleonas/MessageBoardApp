using Autofac;
using MessageApplication.Viewmodel;
using Microsoft.WindowsAzure.MobileServices;

namespace MessageApplication.Bootstrap
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            
            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterType<LoginViewModel>().SingleInstance();
            //cb.RegisterType<MobileServiceClient>().SingleInstance();
        }
    }
}