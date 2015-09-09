using System.Net.Mail;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.Extensions.Internals;
using IDT.RabbitMQ.ServiceClient;
using IDT.TaskActions;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSubscriber
{
    public class ConsumeMessagesAction : RabbitSubscriberTaskAction
    {
        private const string QueueName = "testqueue2";
        private const string MailHost = "INTMAIL.idtdna.com";

        public ConsumeMessagesAction(IModel model)
            : base(model, QueueName)
        {
        }

        public override async Task HandleMessage(DeliveredRabbitMessage message, CancellationToken cancellationToken)
        {
            using (var smtpClient = new SmtpClient(MailHost))
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress("RabbitMQTester@idtdna.com");
                mail.To.Add(new MailAddress("bzell@idtdna.com"));
                mail.Subject = "Test " + Encoding.UTF8.GetString(message.Body);
                smtpClient.Send(mail);
            }

            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}
