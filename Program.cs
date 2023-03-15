
namespace HumWebAPI3
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //Initiate the Web App
            var builder = WebApplication.CreateBuilder(args);
                        
            // Add services to the container.
            builder.Services.AddControllers();

            //Set URL for App
            builder.WebHost.UseUrls(new[] { "https://*:7443" });

            //Add Swagger, because it is great
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            //Add Systemd for future integration
            builder.Services.AddSystemd();
            //builder.Services.AddHostedService<Worker>();

            //Build the Web App
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
                
            });

            app.UseAuthorization();

            app.MapControllers();

            //Run the Contained App
            app.Run();


        }
    }
}