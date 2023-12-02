using LogUtility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LoggerConfig.Configure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 


builder.Services.AddSingleton<SerilogMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseMiddleware<SerilogMiddleware>();

app.UseStaticFiles();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
app.Run();