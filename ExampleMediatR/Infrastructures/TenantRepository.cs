using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleMediatR.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleMediatR.Infrastructures
{
    public class TenantRepository
    {
        private readonly MediatRContext _context;
        public TenantRepository(MediatRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tenant>> GetTenants()
        {
            return await _context.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetTenant(Guid Id) 
        {
            return await _context.Tenants.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<Tenant> CreateTenant(Tenant Tenant)
        {
            _context.Tenants.Add(Tenant);
            await _context.SaveChangesAsync();
            return Tenant;
        }

        public async Task<Tenant> UpdateTenant(Tenant Tenant)
        {
            _context.Tenants.Update(Tenant);
            await _context.SaveChangesAsync();
            return Tenant;
        }

        public async Task<Tenant> DeleteTenant(Tenant Tenant)
        {
            _context.Tenants.Remove(Tenant);
            await _context.SaveChangesAsync();
            return Tenant;
        }
    }
}