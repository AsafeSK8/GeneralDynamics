using AutoMapper;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralDynamics.AI.Application
{
    public class AutoMapperProfileGD : Profile
    {

        /// <summary>
        /// Definición de mapeo de clases de SEG
        /// </summary>
        public AutoMapperProfileGD()
        {

            CreateMap<User, UserDTO>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.LastName))
                .ForMember(x => x.Email, x => x.MapFrom(y => y.Email))
                .ForMember(x => x.Phone, x => x.MapFrom(y => y.Phone))
                .ForMember(x => x.UserName, x => x.MapFrom(y => y.UserName))
                .ForMember(x => x.RoleId, x => x.MapFrom(y => y.RoleId))
                .ForMember(x => x.Role, x => x.MapFrom(y => y.Role.Code));

            //CreateMap<Seguro, SeguroRenovacionDTO>()
            //    .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            //    .ForMember(x => x.IdTomador, x => x.MapFrom(y => y.SeguroFlota.IdTomador))
            //    .ForMember(x => x.Tomador, x => x.MapFrom(y => y.SeguroFlota.Tomador.Alias))
            //    .ForMember(x => x.IdAsegurado, x => x.MapFrom(y => y.SeguroFlota.IdAsegurado))
            //    .ForMember(x => x.Asegurado, x => x.MapFrom(y => y.SeguroFlota.Asegurado.Alias))
            //    .ForMember(x => x.IdProveedor, x => x.MapFrom(y => y.SeguroFlota.IdProveedor))
            //    .ForMember(x => x.Proveedor, x => x.MapFrom(y => y.SeguroFlota.Proveedor.IdNifNavigation.Alias))
            //    .ForMember(x => x.IdMediador, x => x.MapFrom(y => y.SeguroFlota.IdMediador))
            //    .ForMember(x => x.Mediador, x => x.MapFrom(y => y.SeguroFlota.Mediador.IdNifNavigation.Alias))
            //    .ForMember(x => x.IdCompaniaAseguradora, x => x.MapFrom(y => y.SeguroFlota.IdCompaniaAseguradora))
            //    .ForMember(x => x.CompaniaAseguradora, x => x.MapFrom(y => y.SeguroFlota.CompaniaAseguradora.IdNifNavigation.Alias))
            //    .ForMember(x => x.IdTipoSeguro, x => x.MapFrom(y => y.SeguroFlota.IdTipoSeguro))
            //    .ForMember(x => x.TipoSeguro, x => x.MapFrom(y => y.SeguroFlota.TipoSeguro.Descripcion))
            //    .ForMember(x => x.IdSubtipoSeguro, x => x.MapFrom(y => y.SeguroFlota.IdSubtipoSeguro))
            //    .ForMember(x => x.SubTipoSeguro, x => x.MapFrom(y => y.SeguroFlota.SubtipoSeguro.Descripcion))
            //    .ForMember(x => x.IdTipoActivo, x => x.MapFrom(y => y.IdTipoActivo))
            //    .ForMember(x => x.IdUnidadNegocio, x => x.MapFrom(y => y.IdUnidadNegocio))
            //    .ForMember(x => x.UnidadNegocio, x => x.MapFrom(y => y.IdUnidadNegocioNavigation.Descripcion))
            //    .ForMember(x => x.FechaInicio, x => x.MapFrom(y => y.FechaInicio))
            //    .ForMember(x => x.FechaVencimiento, x => x.MapFrom(y => y.FechaVencimiento))
            //    .ForMember(x => x.NumeroPoliza, x => x.MapFrom(y => y.SeguroFlota.NumeroPoliza))
            //    .ForMember(x => x.PrimaNeta, x => x.MapFrom(y => y.SeguroFlota.PrimaNeta))
            //    .ForMember(x => x.PrimaAnual, x => x.MapFrom(y => y.SeguroFlota.PrimaAnual))
            //    .ForMember(x => x.ImporteFranquicia, x => x.MapFrom(y => y.SeguroFlota.ImporteFranquicia))
            //    .ForMember(x => x.Impuestos, x => x.MapFrom(y => y.SeguroFlota.Impuestos))
            //    .ForMember(x => x.Tasa, x => x.MapFrom(y => y.SeguroFlota.Tasa))
            //    .ForMember(x => x.Facturacion, x => x.MapFrom(y => y.SeguroFlota.Facturacion))
            //    .ForMember(x => x.FormaPago, x => x.MapFrom(y => y.SeguroFlota.FormaPago))
            //    .ForMember(x => x.Observaciones, x => x.MapFrom(y => y.SeguroFlota.Observaciones))
            //    .ForMember(x => x.DocumentosAdjuntos, x => x.MapFrom(y => y.SeguroFlota.DocumentosAdjuntos))
            //    .ForMember(x => x.FechaBaja, x => x.MapFrom(y => y.SeguroFlota.FechaBaja))
            //    .ForMember(x => x.ImporteFacturado, x => x.MapFrom(y => y.SeguroFlota.ImporteFacturado))
            //    .ForMember(x => x.IdVehiculo, x => x.MapFrom(y => y.SeguroFlota.id.IdVehiculo))
            //    .ForMember(x => x.Matricula, x => x.MapFrom(y => y.SeguroFlota.Vehiculo.Matricula))
            //    .ForMember(x => x.TipoVehiculo, x => x.MapFrom(y => y.SeguroFlota.VehiculoTipo.Descripcion))
            //    .ForMember(x => x.activo, x => x.MapFrom(y => y.SeguroFlota.activo))
            //    .ReverseMap();
        }
    }
}
