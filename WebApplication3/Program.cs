using WebApplication3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
// Add SignalR services
builder.Services.AddSignalR();

builder.Services.AddControllers();
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

// Use CORS with the specified policy
app.UseCors("CorsPolicy");
// Use default files and static files
app.UseDefaultFiles();
app.UseStaticFiles();
// Map the MessagingHub to the "/hub" endpoint
app.MapHub<MessagingHub>("/hub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
