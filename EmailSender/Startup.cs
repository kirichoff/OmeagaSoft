using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Timers;

namespace EmailSender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

          ;
            services.AddSingleton<EmailSendercs>(new EmailSendercs(new EmailSenderOption(){
                Mail = Configuration.GetSection("Setings").GetSection("Mail").Value,
                MessegeBody = Configuration.GetSection("Setings").GetSection("MessegeBody").Value,
                Password= Configuration.GetSection("Setings").GetSection("Password").Value,
                Resource = Configuration.GetSection("Setings").GetSection("Resource").Value,
                SmptClient = Configuration.GetSection("Setings").GetSection("SmptClient").Value,
                Subject = Configuration.GetSection("Setings").GetSection("Subject").Value,
                Port = Convert.ToInt32( Configuration.GetSection("Setings").GetSection("Port").Value),
                Key = Configuration.GetSection("Setings").GetSection("key").Value
            }));
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
