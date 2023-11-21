using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Package
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //bytes are not auto increment by default
        public byte PackageId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<InventoryInDetail> InventoryInDetails { get; set; } = new List<InventoryInDetail>(); //One to mamy with Details
    }
}