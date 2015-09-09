using System;
using System.Collections.Generic;
using IDT.RabbitMQ.Extensions;
using IDT.TaskActions;
using IDT.TaskActions.Unity;
using Microsoft.Practices.Unity;
using RabbitMQ.Client;
using Topshelf;

namespace TestPublisher
{
    public class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(c =>
            {
                c.SetServiceName("IDTRABBITHUTCHP1");
                c.SetDisplayName("IDT Rabbit Hutch Test Publisher");
                c.SetDescription("Describe your service here.");

                c.Service<TaskActionServiceRunner>(factory =>
                {
                    factory.ConstructUsing(Create);
                    factory.WhenStarted(runner => runner.Start());
                    factory.WhenStopped(runner => runner.Stop());
                })
                 .StartAutomatically()
                 .RunAsLocalSystem();
            });
        }

        public static void LogException(Exception ex)
        {
            Console.WriteLine(ex);
        }

        public static TaskActionServiceRunner Create()
        {
            var factory = new ConnectionFactory
            {
                UserName = "vault",
                Password = "crystal",
                VirtualHost = "/",
                HostName = "mqtest01",
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
                RequestedHeartbeat = 5,
            };

            Func<IUnityContainer> container =
                    () => new UnityContainer()
                            .RegisterInstance<IConnectionFactory>(factory)
                            .RegisterType<IConnection, RabbitMqConnection>(new ContainerControlledLifetimeManager())
                            .RegisterType<IModel, RabbitMqModel>(new HierarchicalLifetimeManager())
                            .RegisterType<ITaskAction, UnityHierarchicalScopedAction<PublisherTaskAction>>("P1")
                            //.RegisterType<ITaskAction, UnityHierarchicalScopedAction<TestPublisher2>>("P2")
                            .RegisterType<IEnumerable<ITaskAction>, ITaskAction[]>();

            return new TaskActionServiceRunner(new UnityContainerScopedAction<ParallelizeActionDecorator>(container), new ExceptionHandler());
        }
    }

    public class ExceptionHandler : IExceptionHandler
    {
        public void HandleException(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
