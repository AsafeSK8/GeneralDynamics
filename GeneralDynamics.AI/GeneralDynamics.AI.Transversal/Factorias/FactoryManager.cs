using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralDynamics.AI.Transversal.Factorias
{
    public static class FactoryManager
    {

        /// <summary>
        /// Lista de mapeos de tipos
        /// </summary>
        private static List<Type> _mappingProfiles = new List<Type>();

        /// <summary>
        /// Invoca la configuración del contexto para todos los elementos de factoría en los ensamblados de los contextos indicados
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo</param>
        /// <param name="dbContextTypes">Tipos de contextos</param>
        public static IServiceCollection ConfigureFactories(this IServiceCollection services, params Assembly[] assemblies)
        {

            //se asignan la colección al manager
            Configure(services, false);

            List<Type> typesToRegister = new List<Type>();
            var itype = typeof(IFactoryConfigurator);

            foreach (var assembly in assemblies)
            {
                var typesInAssembly = assembly.GetTypes().Where(p => itype.IsAssignableFrom(p) && (!p.IsInterface) && (!p.IsAbstract));
                if (typesInAssembly?.Any() ?? false)
                {
                    typesToRegister.AddRange(typesInAssembly);
                }
            }

            if (typesToRegister?.Any() ?? false)
            {
                typesToRegister = typesToRegister.Distinct().OrderBy(c => c.FullName).ToList();
                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    configurationInstance.Configure(services);
                }
            }

            BuildServiceProvider();

            return services;

        }

        /// <summary>
        /// Proveedor de servicios
        /// </summary>
        private static ServiceProvider _servicesProvider = null;

        /// <summary>
        /// Indica el nº de servicios incluidos en el último buildprovider
        /// </summary>
        private static int _lastBuildProviderCount = 0;

        /// <summary>
        /// Constructor estático
        /// </summary>
        /// <remarks></remarks>
        static FactoryManager()
        {
            ServiceCollection = new ServiceCollection();
        }

        /// <summary>
        /// Configura los proveedores de instancias por defecto
        /// </summary>
        /// <remarks></remarks>
        public static void Configure(IServiceCollection services, bool withBuild = false)
        {
            ServiceCollection = services;
            if (withBuild)
            {
                RebuildServiceProvider(true);
            }
        }

        /// <summary>
        /// Devuelve la colección de servicios
        /// </summary>
        public static IServiceCollection ServiceCollection { get; private set; } = null;

        /// <summary>
        /// Verifica si es necesario recompilar los servicios
        /// </summary>
        /// <returns></returns>
        private static bool RebuildProviderNeeded()
        {
            return !((_servicesProvider != null) && (ServiceCollection.Count == _lastBuildProviderCount));
        }

        /// <summary>
        /// Crea el proveedor de servicios con los servicios de la colección
        /// </summary>
        public static ServiceProvider BuildServiceProvider()
        {
            RebuildServiceProvider(true);
            return _servicesProvider;
        }

        /// <summary>
        /// Recrea el proveedor de servicios con los servicios de la colección
        /// </summary>
        /// <param name="forceRebuild">Fuerza el crear el proveedor de servicios</param>
        private static void RebuildServiceProvider(bool forceRebuild = false)
        {
            if ( (forceRebuild) || (RebuildProviderNeeded()))
            {
                var newProvider = ServiceCollection.BuildServiceProvider();                
                _servicesProvider = newProvider;
                _lastBuildProviderCount = ServiceCollection?.Count()??0;                
            }
        }
        
        /// <summary>
        /// Devuelve una instancia del tipo asociado al interfaz
        /// </summary>
        /// <typeparam name="TBase">Tipo base de la instancia que se quiere crear</typeparam>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TBase GetInstance<TBase>()
        {
            try
            {
                return _servicesProvider.GetRequiredService<TBase>();
            } catch (Exception e)
            {
                //error al obtener instancia, se verifica si existe servicio
                if (ServiceCollection != null)
                {
                    if (ServiceCollection.Where(c=>c.ServiceType == typeof(TBase)).Count() > 0)
                    {
                        //existe el tipo en la colección, se reconstruye el servicesProvider
                        RebuildServiceProvider(true);
                        //se reintenta obtener el servicio
                        return _servicesProvider.GetRequiredService<TBase>();
                    }
                }
                throw e;
            }
            
        }


        /// <summary>
        /// Devuelve la configuración mapeada a un objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TConfig GetConfig<TConfig>(string key) where TConfig : new()
        {
            TConfig instance = new TConfig();

            try
            {
                GetInstance<IConfiguration>().GetSection(key).Bind(instance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return instance;
        }

        /// <summary>
        /// Realiza el mapeo de un objeto fuente a otro destino
        /// </summary>
        /// <typeparam name="TSource">Tipo objeto fuente</typeparam>
        /// <typeparam name="TDestination">Tipo objeto destino</typeparam>
        /// <param name="source">Objeto fuente</param>
        /// <param name="destination">Objeto destino</param>
        /// <returns>Objeto mapeado</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source, destination);
            return salida;
        }

        /// <summary>
        /// Realiza el mapeo de un objeto fuente a otro destino
        /// </summary>
        /// <typeparam name="TSource">Tipo objeto fuente</typeparam>
        /// <typeparam name="TDestination">Tipo objeto destino</typeparam>
        /// <param name="source">Objeto fuente</param>
        /// <param name="destination">Objeto destino</param>
        /// <param name="opts">Opciones de mapeo</param>
        /// <returns>Objeto mapeado</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source, destination, opts);
            return salida;
        }

        /// <summary>
        /// Realiza el mapeo de un objeto fuente a otro destino
        /// </summary>
        /// <typeparam name="TSource">Tipo objeto fuente</typeparam>
        /// <typeparam name="TDestination">Tipo objeto destino</typeparam>
        /// <param name="source">Objeto fuente</param>
        /// <returns>Objeto mapeado</returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source);
            return salida;
        }

        /// <summary>
        /// Realiza el mapeo de un objeto fuente a otro destino
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        /// <param name="sourceType">Tipo origen</param>
        /// <param name="destinationType">Tipo destino</param>
        /// <returns>Objeto mapeado</returns>
        public static object Map(object source, Type sourceType, Type destinationType)
        {
            var salida = AutoMapper.Mapper.Map(source, sourceType, destinationType);
            return salida;
        }

        /// <summary>
        /// Realiza el mapeo de una lista de objetos fuente a otra lista de destino
        /// </summary>
        /// <typeparam name="TSource">Tipo objeto fuente</typeparam>
        /// <typeparam name="TDestination">Tipo objeto destino</typeparam>
        /// <param name="source">Lista de objetos fuente</param>
        /// <returns>Lista de objetos mapeada</returns>
        public static IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            IEnumerable<TDestination> salida = AutoMapper.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            if (source is IPagedList)
            {
                IPagedList<TSource> sourcePaged = (IPagedList<TSource>)source;
                salida = new StaticPagedList<TDestination>(salida, sourcePaged.PageNumber, sourcePaged.PageSize, sourcePaged.TotalItemCount);
            }
            return salida;
        }

        /// <summary>
        /// Añade un perfil de mapeo a la factoría
        /// </summary>
        /// <param name="mapperProfile"></param>
        public static void AddMappingProfile(params Type[] mapperProfile)
        {
            if ( (mapperProfile != null) && (mapperProfile.Count() > 0) )
            {
                foreach (var item in mapperProfile)
                {
                    if (!_mappingProfiles.Contains(item))
                    {
                        _mappingProfiles.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve la colección de mapeos
        /// </summary>
        public static List<Type> MappingProfiles
        {
            get
            {
                return _mappingProfiles;
            }
        }

        /// <summary>
        /// Devuelve un tipo a partir de su nombre completo de clase
        /// </summary>
        /// <param name="typeName">Nombre completo de clase</param>
        /// <returns></returns>
        public static Type GetTypeByName(string typeName)
        {
            Type salida = null;
            typeName = typeName?.Trim();
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in assemblies)
            {
                var t = item.GetTypes().Where(c => c.FullName == typeName).FirstOrDefault();
                if (t != null)
                {
                    salida = t;
                    break;
                }
            }
            return salida;
        }

    }
}
