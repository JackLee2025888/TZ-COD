using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using TZ.Service.Models;

namespace Service
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
            #region 添加session
            services.AddSession(options =>
            {
                options.Cookie.Name = ".BIMINNO.Session";
                options.IdleTimeout = System.TimeSpan.FromSeconds(36000);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            #region 跨域
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain",
            builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            #endregion
            //注入 HttpContextAccessor 默认实现了它简化了访问HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion


            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });


            //注入数据库连接
            services.AddDbContext<TZDbContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            //注入配置文件信息
            services.AddSingleton<IConfiguration>(Configuration);

            //启用视图控制器
            services.AddControllersWithViews();

            //启用普通控制器
            services.AddControllers().AddJsonOptions(config =>
            {
                //此设定解决JsonResult中文被编码的问题
                config.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                config.JsonSerializerOptions.PropertyNamingPolicy = null;
                //DateTime格式编码
                //config.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                //config.JsonSerializerOptions.Converters.Add(new DateTimeNullableConvert());
            });


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });

            app.UseSession();//UseSession配置在UseMvc之前

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
