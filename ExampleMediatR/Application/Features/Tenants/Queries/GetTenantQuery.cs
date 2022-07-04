using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleMediatR.Infrastructures;
using AutoMapper;
using MediatR;
using ExampleMediatR.Application.Features.Tenants.Dtos;

namespace ExampleMediatR.Application.Features.Tenants.Queries
{
    public class GetTenantQuery : IRequest<TenantDto?>
    {
        public Guid Id { get; }
        public GetTenantQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetTenantQueryHandler : IRequestHandler<GetTenantQuery, TenantDto?>
    {
        private readonly IMapper _mapper;
        private readonly TenantRepository _tenantContext;
        
        public GetTenantQueryHandler(IMapper mapper, TenantRepository tenantContext)
        {
            _mapper = mapper;
            _tenantContext = tenantContext;
        }

        public async Task<TenantDto?> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            var res = await _tenantContext.GetTenant(request.Id);
            if(res == null)
            {
                return null;
            }
            return _mapper.Map<TenantDto>(res);
        }
    }
}