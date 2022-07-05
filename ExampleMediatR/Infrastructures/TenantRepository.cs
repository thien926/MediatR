using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleMediatR.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleMediatR.Infrastructures
{
    public class TenantRepository
    {
        private readonly MediatRContext _context;
        private readonly IMemoryCache _memoryCache;
        public TenantRepository(MediatRContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Tenant>> GetTenants()
        {
            var res = _memoryCache.Get<IEnumerable<Tenant>>("tenants");
            if(res is null)
            {
                res = await _context.Tenants.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(6)); 
                _memoryCache.Set("tenants", res, cacheEntryOptions);
                await Task.Delay(3000);
            }
            return res;
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