using System.Threading;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ExampleMediatR.Entities;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Infrastructures;


namespace ExampleMediatR.Application.Features.Tenants.Queries
{
    public class GetTenantsQuery : IRequest<IEnumerable<TenantDto>>
    {
        
    }

    public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, IEnumerable<TenantDto>>
    {
        private readonly IMapper _mapper;
        private readonly TenantRepository _tenantContext;
        
        public GetTenantsQueryHandler(IMapper mapper, TenantRepository tenantContext)
        {
            _mapper = mapper;
            _tenantContext = tenantContext;
        }
        
        public async Task<IEnumerable<TenantDto>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            var res = await _tenantContext.GetTenants();
            var list = _mapper.Map<List<TenantDto>>(res);
            return list;
        }
        
        
    }
}