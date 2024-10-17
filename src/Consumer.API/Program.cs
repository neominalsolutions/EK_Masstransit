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
  opt.AddConsumer<SendMessageConsumer>(); // Sadece Dependecy Injection ile Servisin tanýmý için. (Instance üretme kýsmý)

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));

    // Direct Exchange bir kuyruk bekler. çünkü direkt olarak mesajý ilgili kuyruða iletecek.
    // QueueTypes.SendMessageQueue
    config.ReceiveEndpoint(QueueTypes.SendMessageQueue, e =>
    {
      e.ConfigureConsumer<SendMessageConsumer>(context); // Rabbit Mq üzerindne gelen mesaja subscribe olduðumuz ilgili servisi tetiklemesini saðladýðýmýz yer.
    });

  });

  // hangi CLR tipinde mesajý yakalamak yani consume etmek istiyorsak bunu burada consumer olara tanýmlýyoruz.



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
