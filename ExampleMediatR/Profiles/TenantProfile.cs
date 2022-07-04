using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Entities;

namespace ExampleMediatR.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<Tenant, TenantDto>().ReverseMap();
        }
    }
}