using Microsoft.EntityFrameworkCore;
using VirtualMachine.Data.DataContext;
using VirtualMachine.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("VirtualMachineDB");
builder.Services.AddDbContext<VendingMachineDbContext>(q => q.UseSqlServer(conn));
builder.Services.AddScoped<IVendingMachineRepository, VendingMachineRepository>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();