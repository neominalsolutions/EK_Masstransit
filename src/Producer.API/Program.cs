using MassTransit;
using Producer.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// servis registeration k�sm�
builder.Services.AddMassTransit(opt =>
{

  opt.AddConsumer<MessageSubmittedConsumer>();

  opt.UsingRabbitMq((context, config) =>
  {
    config.Host(builder.Configuration.GetConnectionString("RabbitConn"));

    // event dinleme i�lemi yapaca��m�z i�in bizim queue ile bir i�imiz yok, event birden fazla yerden dinlenebilece�i i�in sadece MessageSubmittedConsumer event Listener tan�m� yapmam�z yeterlidir. 
    config.ReceiveEndpoint(x => x.ConfigureConsumer<MessageSubmittedConsumer>(context));

  });


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
