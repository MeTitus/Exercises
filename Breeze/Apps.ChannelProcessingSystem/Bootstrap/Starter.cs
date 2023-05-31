using System;
using System.Threading.Tasks;
using Autofac;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tracers;
using Dywham.Breeze.Fabric.Utils.Bcl;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Bootstrap
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var container = InitializeInfrastructure();
            var channelProcessingEngine = container.Resolve<IChannelProcessingEngine>();

            channelProcessingEngine.AddTracerExecution(new ConsoleChannelProcessingEngineTracer());

            switch (args.Length)
            {
                case 2:
                    await channelProcessingEngine.ProcessAsync(args[0], args[1]);

                    break;
                
                case 0:
                    
                    await channelProcessingEngine.ProcessAsync("Session 22-05-2023 091912111.tdf");
                    
                    break;
                
                default:
                 
                    await channelProcessingEngine.ProcessAsync(args[0]);
                    
                    break;
            }

            Console.ReadKey();
        }

        private static IComponentContext InitializeInfrastructure()
        {
            var builder = new ContainerBuilder();

            //Using all the runtime assemblies in case Persistence and/or Providers are moved to their own projects
            //which as of not, adds no value
            builder.RegisterAssemblyModules(AssemblyUtils.GetRuntimeAssemblies());

            return builder.Build();
        }
    }
}