using AIChatDemo.API.Context;
using AIChatDemo.API.Hubs;
using AIChatDemo.API.Interfaces;
using AIChatDemo.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("https://localhost:7086")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddDbContext<ChatDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddHttpClient<AIChatService>(client =>
{
    var baseUrl = builder.Configuration["ChatGPT:BaseUrl"];
    var apiKey = builder.Configuration["ChatGPT:ApiKey"];


    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
});
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddScoped<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigins");


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatHub");
});


app.MapControllers();

app.Run();
