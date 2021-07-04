using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Revifast.Data;

namespace Revifast.Api
{
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
            services.AddControllers();
            var connection = Configuration.GetConnectionString("DefaultConnection");

            // conexion a la base de datos, referenciar a Revifast.Data
            // se cambia la migracion a Revifast.Data con MigrationsAssembly
            // "Data Source = SQL5092.site4now.net; Initial Catalog = DB_A6B02D_DbRevifast; User Id = DB_A6B02D_DbRevifast_admin; Password = revifast1",
            // "Data Source = DESKTOP-1ACUFB0; Database = myDataBase; Trusted_Connection = True;",

            services.AddDbContext<DbRevifastContext>(options =>
            options.UseSqlServer(
                connection, migration => migration.MigrationsAssembly("Revifast.Api")));

            services.AddCors(options => options.AddPolicy("pelusa", builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c=>
            { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

        }
    }
}
