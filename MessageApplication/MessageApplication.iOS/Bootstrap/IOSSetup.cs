using Autofac;
using MessageApplication.Bootstrap;
using MessageApplication.iOS.Services;
using MessageApplication.Services;

namespace MessageApplication.iOS.Bootstrap
{
    public class IosSetup:AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder cb)
        {
            base.RegisterDependencies(cb);
            cb.RegisterType<PlatformService>().As<IPlatformService>();
        }
    }
}