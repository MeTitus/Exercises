using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.File;
using Dywham.Breeze.Fabric.Adapters.IO;
using Dywham.Breeze.Fabric.Adapters.IO.Compression;
using Dywham.Breeze.Fabric.Utils.Bcl;

namespace Dywham.Breeze.Fabric.Adapters.DataAcquisition.Infrastructure
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataAcquisitionAdapter>()
            .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<InputOutputAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ZipFileAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<UniqueIdentifierGeneratorAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<FileDataAcquisitionAccess>()
                .As<IFileDataAcquisitionConverter>()
                .SingleInstance();

            var assemblies = AssemblyUtils.GetRuntimeAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t is { IsClass: true } && typeof(IDataAcquisitionAccess).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.Register<IDataAcquisitionAccess>((c, p) =>
                {
                    var value = ((ConstantParameter)((Parameter[])p).First()).Value;

                    if (value is string)
                    {
                        return c.Resolve<IFileDataAcquisitionAccess>();
                    }

                    throw new ArgumentException("Invalid access type");
                })
                .As<IDataAcquisitionAccess>();
        }
    }
}
