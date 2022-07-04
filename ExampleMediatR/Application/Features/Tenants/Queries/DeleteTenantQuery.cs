using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Infrastructures;
using MediatR;

namespace ExampleMediatR.Application.Features.Tenants.Queries
{
    public class DeleteTenantQuery : IRequest<TenantDto?>
    {
        public Guid Id { get; }
        
        public DeleteTenantQuery(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteTenantQueryHandler : IRequestHandler<DeleteTenantQuery, TenantDto?>
    {
        private readonly IMapper _mapper;
        private readonly TenantRepository _tenantContext;
        
        public DeleteTenantQueryHandler(IMapper mapper, TenantRepository tenantContext)
        {
            _mapper = mapper;
            _tenantContext = tenantContext;
        }
        public async Task<TenantDto?> Handle(DeleteTenantQuery request, CancellationToken cancellationToken)
        {
            var tempTenant = await _tenantContext.GetTenant(request.Id);
            if(tempTenant == null) 
            {
                return null;
            }
            
            await _tenantContext.DeleteTenant(tempTenant);

            return _mapper.Map<TenantDto>(tempTenant);
        }
    }
}