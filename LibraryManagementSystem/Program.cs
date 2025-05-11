using LibraryManagementSystem.Data;
using LibraryManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Register repositories & services
builder.Services.AddSingleton<BookRepository>();
builder.Services.AddSingleton<BookService>();
builder.Services.AddSingleton<SubscriptionRepository>();     
builder.Services.AddSingleton<SubscriptionService>();         

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();