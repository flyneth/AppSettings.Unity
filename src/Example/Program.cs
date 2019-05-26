using System;
using Newtonsoft.Json;
using Unity;
using Unity.AppSettings;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = UnityHelper.Container;

            // Register your type's mappings here.
            container.RegisterAppSettings<CustomAppSettings>();

            var appSettings = container.Resolve<CustomAppSettings>();

            Console.WriteLine(JsonConvert.SerializeObject(appSettings));

            Console.ReadKey();
        }
    }
}
