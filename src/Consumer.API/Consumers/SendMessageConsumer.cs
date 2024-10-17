using MassTransit;
using Messaging.Contracts;

namespace Consumer.API.Consumers
{
  public class SendMessageConsumer : IConsumer<SendMessage>
  {
    private readonly ILogger<SendMessageConsumer> logger;
    // event fırlatma süreçlerini bu API üzerinden işeyeceğiz.
    private readonly IPublishEndpoint publishEndpoint;

    public SendMessageConsumer(ILogger<SendMessageConsumer> logger, IPublishEndpoint publishEndpoint)
    {
      this.logger = logger;
      this.publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<SendMessage> context)
    {
      
      this.logger.LogInformation($"Message: {context.Message.message}, messageId: {context.MessageId}");
      // burada ise Integration Event fırlattık.
      // Sistemler arası distributed olarak haberleşeceğimiz eventimiz.
      await this.publishEndpoint.Publish(new MessageSubmitted(context.Message.message));

     
    }
  }
}
