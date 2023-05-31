using Autofac;
using Dywham.Breeze.Fabric.Utils.Bcl;

namespace Dywham.Breeze.Fabric.Persistence.Repositories.Infrastructure
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyUtils.GetRuntimeAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t is { IsClass: true } && typeof(IPersistenceEntry).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .SingleInstance();

            //Avoids the need to have to declare an interface for every repository

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t is { IsClass: true } && typeof(IRepository).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
