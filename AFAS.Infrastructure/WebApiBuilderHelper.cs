using Microsoft.Extensions.DependencyInjection;
using Mr1Ceng.Util;
using System.Reflection;

namespace WingWell.Infrastructure;

/// <summary>
/// WebApi Builder Helper
/// </summary>
public class WebApiBuilderHelper
{
    /// <summary>
    /// 业务类注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblyNames"></param>
    public static void RegistBusinessInterface(IServiceCollection services, List<string> assemblyNames)
    {
        foreach (var assemblyName in assemblyNames)
        {
            if (assemblyName != "")
            {
                var assembly = Assembly.Load(assemblyName);
                var classList = assembly.GetTypes().Where(x => x.IsClass && x.Name.EndsWith("Service")).ToList();
                var interfaceList = assembly.GetTypes()
                    .Where(x => x.IsInterface && x.Name.StartsWith('I') && x.Name.EndsWith("Service")).ToList();
                foreach (var classItem in classList)
                {
                    var interfaceItem = interfaceList.FirstOrDefault(x
                        => x.Name == GetString.FromObject(classItem.GetTypeInfo().ImplementedInterfaces.FirstOrDefault()?.Name)
                    );
                    if (interfaceItem != null)
                    {
                        services.Add(new ServiceDescriptor(interfaceItem, classItem, ServiceLifetime.Transient));
                    }
                }
            }
        }
    }
}
