using System.Text.Json;
using Microsoft.AspNetCore.OData;
using RL.Data;
using MediatR; 
using RL.BackEnd.Data;
using RL.Data.DataModels;
using RL.BackEnd.BusinessService.UserService;
using RL.BackEnd.BusinessService.ProcedureService;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()); 
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(typeof(UserServiceCommonHandler));
builder.Services.AddTransient<IUserService, UserServiceCommonHandler>();
builder.Services.AddTransient<IProcedureService,ProcedureServiceCommonHandler>();

builder.Services.AddTransient<IRepository<User>, Repository<User>>();
builder.Services.AddTransient<IRepository<Procedure>, Repository<Procedure>>();

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddSqlite<RLContext>("Data Source=Database.db");
//builder.Services.AddTransient<IPlanService, PlanServices>();
builder.Services.AddControllers()
    .AddOData(options => options.Select().Filter().Expand().OrderBy())
    .AddJsonOptions(options => options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<EnableQueryFiler>();
});
var corsPolicy = "allowLocal";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
    policy =>
    {
        //  policy.WithOrigins("http://localhost:3001").AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RL v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();