using Consumer.API.Consumers;
using MassTransit;
using Messaging.RabbitMqConsts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
  opt.AddConsumer<SendMessageConsumer>(); // Sadece Dependecy Injection ile Servisin tan�m� i�in. (Instance �retme k�sm�)

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));

    // Direct Exchange bir kuyruk bekler. ��nk� direkt olarak mesaj� ilgili kuyru�a iletecek.
    // QueueTypes.SendMessageQueue
    config.ReceiveEndpoint(QueueTypes.SendMessageQueue, e =>
    {
      e.ConfigureConsumer<SendMessageConsumer>(context); // Rabbit Mq �zerindne gelen mesaja subscribe oldu�umuz ilgili servisi tetiklemesini sa�lad���m�z yer.
    });

  });

  // hangi CLR tipinde mesaj� yakalamak yani consume etmek istiyorsak bunu burada consumer olara tan�ml�yoruz.



});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
