using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Package
    {
        public byte PackageId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<InventoryInDetail> InventoryInDetails { get; set; } = new List<InventoryInDetail>(); //One to mamy with Details
    }
}