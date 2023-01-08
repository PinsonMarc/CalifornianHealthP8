using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Booking.API;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking-api", Version = "v1" });
        });

        services.AddScoped<IRepository, Repository>();
        services.AddControllers();
        services.AddDbContext<CHDBContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("CHDBContext"))
        );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (IServiceScope scope = app.ApplicationServices.CreateScope())
        {
            using CHDBContext? context = scope.ServiceProvider.GetService<CHDBContext>();
            if (context is not null) context.Database.EnsureCreated();
        }

        app.UseSwagger();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking-api");
        });
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}

