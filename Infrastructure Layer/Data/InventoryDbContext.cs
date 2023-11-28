using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data
{
    public class InventoryDbContext: DbContext
    {
        public DbSet<InventoryInHeader> InventoryInHeaders {get; set;}
        public DbSet<InventoryInDetail> InventoryInDetails { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Item> Items { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options): base(options) {}
    }
}