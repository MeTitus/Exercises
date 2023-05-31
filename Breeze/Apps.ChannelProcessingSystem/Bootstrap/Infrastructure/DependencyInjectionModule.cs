using System.IO;
using Autofac;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;
using Dywham.Breeze.Fabric.Adapters;
using Dywham.Breeze.Fabric.Adapters.Maths.Equations;
using Dywham.Breeze.Fabric.Adapters.Regex;
using Dywham.Breeze.Fabric.Adapters.Serialization.Json;
using Dywham.Breeze.Fabric.Utils.Bcl;
using Newtonsoft.Json;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Bootstrap.Infrastructure
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyUtils.GetRuntimeAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t is { IsClass: true } && typeof(IProvider).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<ChannelProcessingEngine>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<DateTimeAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<MathEquationsAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<RegexAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<JsonSerializerAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<CalculatorProvider>()
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .OnActivated(x => x.Instance.InitializeAsync().ConfigureAwait(true).GetAwaiter().GetResult());

            builder.RegisterInstance(JsonConvert.DeserializeObject<EndpointSettings>(File.ReadAllText("EndpointSettings.json")))
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
