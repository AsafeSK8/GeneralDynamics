
using AutoMapper;
using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.Extensions.DependencyInjection;
using PagedList;
using System;
using System.Collections.Generic;

namespace GeneralDynamics.AI.Application
{

    /// <summary>
    /// Clase de configuración de la Factoria para MAN
    /// </summary>
    public class FactoryGD : FactoryConfigurator
    {

        /// <summary>
        /// Método de configuración de los servicios y mapeos necesarios por MAN
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <returns>Colección de servicios con los servicios de MAN incluidos</returns>
        public override IServiceCollection Configure(IServiceCollection services)
        {
            FactoryManager.AddMappingProfile(typeof(AutoMapperProfileGD));

            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }


        /// <summary>
        /// Devuelve una instancia del tipo asociado al interfaz
        /// </summary>
        /// <typeparam name="TBase">Tipo base de la instancia que se quiere crear</typeparam>
        /// <returns></returns>
        /// <remarks></remarks>
        public static TBase GetInstance<TBase>()
        {
            return FactoryManager.GetInstance<TBase>();
        }

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

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source, destination);
            return salida;
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source, destination, opts);
            return salida;
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            TDestination salida = AutoMapper.Mapper.Map<TSource, TDestination>(source);
            return salida;
        }

    }
}
