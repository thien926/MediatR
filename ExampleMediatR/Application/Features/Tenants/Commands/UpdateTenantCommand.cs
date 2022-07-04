using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ExampleMediatR.Entities;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Infrastructures;

namespace ExampleMediatR.Application.Features.Tenants.Commands
{
    public class UpdateTenantCommand : TenantDto, IRequest<TenantDto?>
    {
    }

    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, TenantDto?>
    {
        private readonly IMapper _mapper;
        private readonly TenantRepository _tenantContext;
        
        public UpdateTenantCommandHandler(IMapper mapper, TenantRepository tenantContext)
        {
            _mapper = mapper;
            _tenantContext = tenantContext;
        }

        public async Task<TenantDto?> Handle(UpdateTenantCommand command, CancellationToken cancellationToken)
        {
            var tempTenant = await _tenantContext.GetTenant(command.Id);
            if(tempTenant == null) 
            {
                return null;
            }

            tempTenant.Name = command.Name;
            await _tenantContext.UpdateTenant(tempTenant);
            return command;
        }
    }
}