using TPCadastroUsuario.Adapters.Drivers.WebApi.Extensions;
using TPCadastroUsuario.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDocumentacaoSwagger();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddCors(opt => {
    opt.AddPolicy("AllowAngular", policy => {
        policy
          .WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandling();

app.UseCors("AllowAngular");

app.UsarSwaggerSomenteEmDev();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

