using System.Diagnostics;
using System.IO;
using IDT.Common;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.ServiceClient;
using IDT.TaskActions;
using IDT.TaskActions.Unity;
using log4net.Config;
using Microsoft.Practices.Unity;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using Topshelf;
using Topshelf.HostConfigurators;

namespace TestSubscriber
{
    public class Program
    {
        static int Main(string[] args)
        {
            XmlConfigurator.Configure();

            var applicationInfo = ApplicationInfoConfiguration.Load();
            
            return (int)HostFactory.Run(c =>
            {
                //TODO: Update below values. Note that ServiceName must contain no special characters.
                c.SetServiceName("IDTRABBITHUTCHS2");
                c.SetDisplayName("IDT Rabbit Hutch Test Subscriber");
                c.SetDescription("Describe your service here.");

                //TODO: Update values.  Must match values for log4net applicationName and logName in App.config
                c.AfterInstall(() => EventLog.CreateEventSource("YourService", "IDTSvcs"));

                c.UseLog4Net();

                c.Service<TaskActionServiceRunner>(factory =>
                {
                    factory.ConstructUsing(() => new UnityTaskActionServiceRunner<ConsumeMessagesAction>(() => CreateContainer(applicationInfo), new Log4NetExceptionHandler()));
                    factory.WhenStarted(runner => runner.Start());
                    factory.WhenStopped(runner => runner.Stop());
                })
                 .StartAutomatically()
                 .RunAsLocalSystem();
            });
        }

        public static IUnityContainer CreateContainer(IApplicationInfo appInfo)
        {
            var regClient = new RegistryServiceClient(appInfo);
            var hostName = regClient.GetResourceLocation(RegistryResourceType.ServiceHost, "rabbitmq", 1);

            var factory = new ConnectionFactory
            {
                UserName = "vault",
                Password = "crystal",
                VirtualHost = "/",
                HostName = hostName,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
                RequestedHeartbeat = 5,
            };

            return new UnityContainer()
                        .RegisterInstance<IConnectionFactory>(factory)
                        .RegisterType<IConnection, RabbitMqConnection>(new ContainerControlledLifetimeManager())
                        .RegisterType<IModel, RabbitMqModel>(new HierarchicalLifetimeManager());
        }
    }
}
