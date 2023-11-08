using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class InventoryInDetailDto
    {
        public int InventoryInDetailId { get; set; }
        public InventoryInHeaderDto InventoryInHeader { get; set; } = null!;
        public int Serial { get; set; }
        public ItemDto Item { get; set; } = null!;
        public PackageDto Package { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateOnly ExpireDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal ConsumerPrice { get; set; }
        public decimal TotalValue => Quantity * ConsumerPrice; //This is where we compute the TotalValue in the detailed inventory header

        public static InventoryInDetailDto FromEntity(InventoryInDetail e){
            return new InventoryInDetailDto() {
                InventoryInDetailId = e.InventoryInDetailId,
                InventoryInHeader = InventoryInHeaderDto.FromEntity(e.InventoryInHeader),
                Serial = e.Serial,
                Item = ItemDto.FromEntity(e.Item),
                Package = PackageDto.FromEntity(e.Package),
                BatchNumber = e.BatchNumber,
                SerialNumber = e.SerialNumber,
                ExpireDate = e.ExpireDate,
                Quantity = e.Quantity,
                ConsumerPrice = e.ConsumerPrice,
                //TotalValue = e.TotalValue
            };
        }
    }
}