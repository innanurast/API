using Amazon.AWSSupport.Model;
using API.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", Options => Options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//register dbcontext
builder.Services.AddDbContext<MyContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIContext")));

builder.Services.AddScoped<EmployeesRepository>();
builder.Services.AddScoped<AccountRepository>();

//builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfiguration"));

//var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetService<IConfiguration>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
