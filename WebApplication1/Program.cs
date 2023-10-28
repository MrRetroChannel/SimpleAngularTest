using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MSSQLDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnect"))
);

builder.Services.AddCors(cors => cors.AddPolicy("AllowOrigin",
            options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
        );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
