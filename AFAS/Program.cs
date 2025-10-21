using AFAS.Authorization;
using AFAS.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Mr1Ceng.Util.Swagger;
using System.Reflection;
using AFAS.Infrastructure;
using AFAS.WebApi.Platform;

SystemConfig.Setup(Assembly.GetExecutingAssembly().GetName().Name); //ϵͳ��ʼ��

#region ע�����
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(option => { option.Filters.Add<ExceptionResponseFilter>(); }); //�쳣����
builder.Services.AddCors(options => options.AddPolicy(SystemConfig.SystemId,
    p => p
    //.WithOrigins(SystemConfig.CorsUrls)
    .AllowAnyOrigin()
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowAnyHeader()
    .WithMethods("GET", "POST"))); //����
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100MB
});
#endregion

#region ҵ����ע��

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthInfo, AuthInfo>();
WebApiBuilderHelper.RegistBusinessInterface(builder.Services, WebApiConfig.BusinessAssemblyNames);

#endregion

#region Swagger

//����API����
SwaggerHelper.Config(builder.Services, WebApiConfig.SwaggerConfig);

#endregion

var app = builder.Build();

SwaggerHelper.Apply(app, WebApiConfig.SwaggerConfig);
// ���þ�̬�ļ��м��
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "../AFAS.Static/")),
    RequestPath = "/Static", // �Զ������·��ǰ׺
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
    }
});

app.UseCors(SystemConfig.SystemId); //����
app.UseAuthorization();

app.MapControllers();

app.Run();
