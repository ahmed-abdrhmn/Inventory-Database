using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Branch
    {
        public short BranchId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<InventoryInHeader> InventoryInHeaders { get; set; } = new List<InventoryInHeader>(); //Assuming One to many relationship btw branch and header
    }
}