using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            HostingEnvironment = env;
        }


        public IHostingEnvironment HostingEnvironment { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.0.0", new Info
                    {
                        Version = "1.0.0",
                        Title = "Number Theory",
                        Description = "Number Theory Calculation Service (ASP.NET Core 2.1)",
                        Contact = new Contact()
                        {
                            Name = "Stefan Ilic",
                            Email = "stefan.ilic.vie@gmail.com"
                        },
                    });
                });

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddSingleton<IDataAccessLayer>(s => new DevDal(Configuration.GetConnectionString("StefansLocalSqlite")));
            }
            else
            {
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseMvc()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "Number Theory Service");
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
