using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public class InventoryInHeader
    {
        public int InventoryInHeaderId {get; set;} //TypeNameId is automatically primary key
        public Branch Branch {get; set;} = null!; //should automatically create the foreign key
        public DateOnly DocDate { get; set; }
        [MaxLength(50)]
        public string Reference { get; set; } = null!;
        //computed fields should not be included in database
        //[Precision(18,6)]
        //public decimal TotalValue { get; set; }
        [MaxLength(300)]
        public string Remarks { get; set; } = null!;
        public ICollection<InventoryInDetail> InventoryInDetails { get; set; } = new List<InventoryInDetail>();
    }
}