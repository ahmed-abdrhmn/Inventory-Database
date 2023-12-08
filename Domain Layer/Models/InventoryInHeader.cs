using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class InventoryInHeader: AggregateRoot
    {
        //public int InventoryInHeaderId {get; set;} //TypeNameId is automatically primary key
        public Branch Branch {get; set;} = null!; //should automatically create the foreign key
        public DateOnly DocDate { get; set; }
        [MaxLength(50)]
        public string Reference { get; set; } = null!;
        [Precision(18,6)]
        public decimal TotalValue { get; set; }
        [MaxLength(300)]
        public string Remarks { get; set; } = null!;
        public ICollection<InventoryInDetail> InventoryInDetails { get; set; } = new List<InventoryInDetail>();
    }
}