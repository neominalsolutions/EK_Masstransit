using MassTransit;
using Messaging.Contracts;

namespace Notification.API.Consumers
{
  public class MessageSubmittedConsumer : IConsumer<MessageSubmitted>
  {
    public async Task Consume(ConsumeContext<MessageSubmitted> context)
    {
       await Task.CompletedTask;
    }
  }
}
