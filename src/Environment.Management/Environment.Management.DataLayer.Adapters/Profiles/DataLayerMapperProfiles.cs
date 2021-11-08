using AutoMapper;
using Environment.Management.Domain.Entities;
using DbEntities = Environment.Management.DataLayer.Entities;

namespace Environment.Management.Domain.Profiles
{
    public class DataLayerMapperProfiles : Profile
    {
        public DataLayerMapperProfiles()
        {
            CreateMap<DbEntities.Tenant, Tenant>().ReverseMap();
            CreateMap<DbEntities.BaseEntity, BaseEntity>().ReverseMap();
            CreateMap<DbEntities.TenantFeature, TenantFeature>().ReverseMap();
            CreateMap<DbEntities.TenantState, TenantState>().ReverseMap();
        }
    }
}