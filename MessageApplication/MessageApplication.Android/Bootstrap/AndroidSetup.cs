using Autofac;
using MessageApplication.Android.Services;
using MessageApplication.Bootstrap;
using MessageApplication.Services;

namespace MessageApplication.Android.Bootstrap
{
    public class AndroidSetup:AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder cb)
        {
            base.RegisterDependencies(cb);
            cb.RegisterType<PlatformService>().As<IPlatformService>();
        }
    }
}