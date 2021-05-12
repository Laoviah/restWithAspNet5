using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithAspNet5.Business;
using RestWithAspNet5.Business.Implementation;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Repository.Implementation;
using Serilog;
using System.Collections.Generic;

namespace RestWithAspNet5
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers();

            var connection = Configuration["MySqlConnection:MySqlConnectionString"];

            if (Environment.IsDevelopment())
            {
                MigrateDataBase(connection);
            }

            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            services.AddApiVersioning();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
            services.AddScoped<IBooksBusiness, BooksBusinessImplementation>();
            services.AddScoped<IBookRepository, BookRepositoryImplementation>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void MigrateDataBase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg)) { 
                    Locations = new List<string> { "database/migrations", "database/dataset" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (System.Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }


    }
}
