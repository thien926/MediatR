using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleMediatR.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleMediatR.Infrastructures
{
    public class MediatRContext : DbContext
    {
        public MediatRContext(DbContextOptions<MediatRContext> options) : base(options)
        {
            
        }

        public DbSet<Tenant> Tenants { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(256);
            });
        }
    }
}