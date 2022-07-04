using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MediatR;
using ExampleMediatR.Entities;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Infrastructures;


namespace ExampleMediatR.Application.Features.Tenants.Commands
{
    public class CreateTenantCommand : TenantDto, IRequest<TenantDto>
    {
    }

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, TenantDto>
    {
        private readonly IMapper _mapper;
        private readonly TenantRepository _tenantContext;
        
        public CreateTenantCommandHandler(IMapper mapper, TenantRepository tenantContext)
        {
            _mapper = mapper;
            _tenantContext = tenantContext;
        }

        public async Task<TenantDto> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
        {
            var tenant = _mapper.Map<Tenant>(command);
            await _tenantContext.CreateTenant(tenant);
            return command;
        }
    }
}