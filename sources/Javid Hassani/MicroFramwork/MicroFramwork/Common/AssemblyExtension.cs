using MicroFramwork.Controllers;
using MicroFramwork.Exceptions;
using System.Reflection;

namespace MicroFramwork.Common;

public static class AssemblyExtension
{
    public static Type GetFromAssembly(this string controllerName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        // Specify the base type
        Type baseType = typeof(BaseController);

        // Get all types in the assembly that inherit from the base type
        var controller = assembly.GetTypes()
                                   .Where(t => baseType.IsAssignableFrom(t)
                                   && t != baseType
                                   && t.Name == $"{controllerName}Controller").FirstOrDefault();

        if (controller is null)
            throw new TypeNotFoundException($"invalid controller not found {controllerName}Controller");

        return controller;
    }
}
