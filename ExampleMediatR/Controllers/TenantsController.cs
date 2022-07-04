using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExampleMediatR.Application.Features.Tenants.Dtos;
using ExampleMediatR.Entities;
using ExampleMediatR.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ExampleMediatR.Application.Features.Tenants.Queries;
using ExampleMediatR.Application.Features.Tenants.Commands;

namespace ExampleMediatR.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        // private readonly ILogger<TenantsController> _logger;

        // public TenantsController(ILogger<TenantsController> logger)
        // {
        //     _logger = logger;
        // }

        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetTenants()
        {
            var query = new GetTenantsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantDto>> GetTenant(Guid Id) 
        {
            var query = new GetTenantQuery(Id);
            var result = await _mediator.Send(query);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TenantDto>> CreateTenant(CreateTenantCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return result;
            }
            catch(Exception e) 
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TenantDto>> UpdateTenant(Guid Id, UpdateTenantCommand command)
        {
            if(Id != command.Id) 
            {
                return BadRequest();
            }

            try 
            {
                var result = await _mediator.Send(command);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch(Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTenant(Guid Id)
        {
            try{
                var query = new DeleteTenantQuery(Id);
                var result = await _mediator.Send(query);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch(Exception e)
            {
                return NotFound(e);
            }
        }
    }
}