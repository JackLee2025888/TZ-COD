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
            #region ���session
            services.AddSession(options =>
            {
                options.Cookie.Name = ".BIMINNO.Session";
                options.IdleTimeout = System.TimeSpan.FromSeconds(36000);//����session�Ĺ���ʱ��
                options.Cookie.HttpOnly = true;//���������������ͨ��js��ø�cookie��ֵ
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            #region ����
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain",
            builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            #endregion
            //ע�� HttpContextAccessor Ĭ��ʵ���������˷���HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion


            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });


            //ע�����ݿ�����
            services.AddDbContext<TZDbContext>(options =>
                    options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            //ע�������ļ���Ϣ
            services.AddSingleton<IConfiguration>(Configuration);

            //������ͼ������
            services.AddControllersWithViews();

            //������ͨ������
            services.AddControllers().AddJsonOptions(config =>
            {
                //���趨���JsonResult���ı����������
                config.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                config.JsonSerializerOptions.PropertyNamingPolicy = null;
                //DateTime��ʽ����
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

            app.UseSession();//UseSession������UseMvc֮ǰ

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
