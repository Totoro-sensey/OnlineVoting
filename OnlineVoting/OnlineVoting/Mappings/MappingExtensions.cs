using System.Reflection;
using AutoMapper;

namespace OnlineVoting.Mappings
{
    public static class MappingExtensions
    {
        public static void ApplyMappingsFromAssembly(this Profile profile, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t != typeof(IMapFrom<>) && t != typeof(IMapTo<>)
                                                    && t.GetInterfaces().Contains(typeof(IMapped)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapped")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { profile });
            }
        }
    }
}
