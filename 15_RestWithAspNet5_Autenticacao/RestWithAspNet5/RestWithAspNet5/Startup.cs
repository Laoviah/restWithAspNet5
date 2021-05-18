using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithAspNet5.Business;
using RestWithAspNet5.Business.Implementation;
using RestWithAspNet5.Hypermedia.Enricher;
using RestWithAspNet5.Hypermedia.Filters;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Repository.Generic;
using Serilog;
using System;
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

            services.AddCors(options => options.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            var connection = Configuration["MySqlConnection:MySqlConnectionString"];

            if (Environment.IsDevelopment())
            {
                MigrateDataBase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);

            services.AddApiVersioning();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                                                        Title = "REST API's From 0 to Azure With Docker",
                                                        Version = "v1",
                                                        Description= "API RESTFul developed in course 'REST API's From 0 to Azure with ASP.NET 5' ",
                                                        Contact = new OpenApiContact
                                                        {
                                                            Name = "Vially Israel",
                                                            Url = new Uri("https://github.com/Laoviah")
                                                        }
                                                   }
                );
            });

            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBooksBusiness, BooksBusinessImplementation>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET From 0 to Azure with Docker");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id}");
            });
        }

        private void MigrateDataBase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
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
