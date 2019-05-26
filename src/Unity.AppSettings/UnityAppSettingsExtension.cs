using System;
using System.Collections.Generic;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.AppSettings
{
    public static class UnityAppSettingsExtension
    {
        /// <summary>
        /// Register a <see cref="LifetimeManager"/> for the given type with the container.
        /// Map appsettings section into specified model (<see cref="T"/>).
        /// </summary>
        /// <typeparam name="T">The type to apply the <paramref name="lifetimeManager"/> to.</typeparam>
        /// <param name="container">Container to configure.</param>
        /// <param name="lifetimeManager">
        ///     This interface marks all lifetime managers compatible with 
        /// <see cref="IUnityContainer.RegisterType" /> registration.
        /// <remarks>
        ///     This interface is used for design type validation of registration compatibility.
        ///     Each registration type only takes lifetime managers compatible with it. 
        /// </remarks>
        /// </param>
        /// <returns>The <see cref="Unity.IUnityContainer"/> object that this method was called on.</returns>
        public static IUnityContainer RegisterAppSettings<T>(this IUnityContainer container, ITypeLifetimeManager lifetimeManager = null) where T : class
        {
            var settingType = typeof(T);

            List<InjectionMember> injectionMembers = new List<InjectionMember>();
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;

            foreach (var settingPropertyInfo in settingType.GetProperties())
            {
                injectionMembers.Add(new InjectionProperty(
                    settingPropertyInfo.Name,
                    Convert.ChangeType(appSettings[settingPropertyInfo.Name],
                        Nullable.GetUnderlyingType(settingPropertyInfo.PropertyType) ?? settingPropertyInfo.PropertyType))
                    );
            }

            if (injectionMembers != null && injectionMembers.Count > 0)
            {
                container.RegisterType<T>(lifetimeManager ?? new ContainerControlledLifetimeManager(), injectionMembers.ToArray());
            }

            return container;
        }
    }
}
