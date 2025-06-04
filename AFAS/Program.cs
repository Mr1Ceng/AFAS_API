using AFAS.Authorization;
using AFAS.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Mr1Ceng.Util.Swagger;
using System.Reflection;
using WingWell.Infrastructure;
using WingWell.WebApi.Platform;

SystemConfig.Setup(Assembly.GetExecutingAssembly().GetName().Name); //系统初始化

#region 注册服务
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(option => { option.Filters.Add<ExceptionResponseFilter>(); }); //异常处理
builder.Services.AddCors(options => options.AddPolicy(SystemConfig.SystemId,
    p => p
    //.WithOrigins(SystemConfig.CorsUrls)
    .AllowAnyOrigin()
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowAnyHeader()
    .WithMethods("GET", "POST"))); //跨域
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100MB
});
#endregion

#region 业务类注入

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthInfo, AuthInfo>();
WebApiBuilderHelper.RegistBusinessInterface(builder.Services, WebApiConfig.BusinessAssemblyNames);

#endregion

#region Swagger

//配置API服务
SwaggerHelper.Config(builder.Services, WebApiConfig.SwaggerConfig);

#endregion

var app = builder.Build();

SwaggerHelper.Apply(app, WebApiConfig.SwaggerConfig);
// 配置静态文件中间件
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "../AFAS.Static/")),
    RequestPath = "/Static", // 自定义访问路径前缀
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
    }
});

app.UseCors(SystemConfig.SystemId); //跨域
app.UseAuthorization();

app.MapControllers();

app.Run();
