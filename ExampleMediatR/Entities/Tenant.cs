using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMediatR.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}