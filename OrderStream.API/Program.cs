using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderStream.Application.Services;
using Serilog;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        
        builder.Host.UseSerilog();
        builder.Services.AddSingleton(Log.Logger);
        // builder.Services.AddDbContext<AppDbContext>(options => 
        // {
        //     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
        // });
        builder.Services.Configure<MongoDBSettings>(
            builder.Configuration.GetSection("MongoDB"));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
        builder.Services.AddSingleton<MongoDbContext>();

        // Services and Repositories
        builder.Services.AddScoped<IOrderService,OrderService>(); 
        builder.Services.AddScoped<IOrderRepository, OrderRepositoryAdapter>();
        builder.Services.AddScoped<IMongoOrderRepository, MongoOrderRepository>();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddAutoMapper(typeof(OrderMappingProfile));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers(); 
        app.Run();

    }
}
