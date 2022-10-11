using AutoMapper;
using GeneralDynamics.AI.Transversal.Factorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralDynamics.AI.Application
{
    public class AutoMapperConfig
    {
     
        /// <summary>
        /// Inicializar los Profile de Automapper
        /// </summary>
        /// <param name="listaAutomapperProfile"></param>
        public static void RegisterMappings(IEnumerable<Type> listaAutomapperProfile = null)
        {            
            try
            {
                if (listaAutomapperProfile?.Any() ?? false)
                {
                    FactoryManager.AddMappingProfile(listaAutomapperProfile.ToArray());
                }
                FactoryManager.AddMappingProfile(typeof(AutoMapperProfileGD));

                //Inicializar Automapper
                IEnumerable<Type> listaProfile = FactoryManager.MappingProfiles;
                if (listaProfile?.Any() ?? false)
                {
                    Mapper.Initialize(a => a.AddProfiles(listaProfile));
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
