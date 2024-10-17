using MassTransit;
using Messaging.Contracts;
using Messaging.RabbitMqConsts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Producer.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MessagesController : ControllerBase
  { // mesaj göndereceğimiz kuyruk tanımlaması yapıyoruz.
    private readonly ISendEndpointProvider sendEndpointProvider;
    public MessagesController(ISendEndpointProvider sendEndpointProvider)
    {
      this.sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost] // Order Service
    public async Task<IActionResult> SubmitMessage()
    {
      var uri = new Uri($"queue:{QueueTypes.SendMessageQueue}");
      // api endpoint benzer broker endpoint tanımı
      var endpoint = await this.sendEndpointProvider.GetSendEndpoint(uri);
      await endpoint.Send(new SendMessage("Mesaj"));


      return Ok();
    }
  }
}
