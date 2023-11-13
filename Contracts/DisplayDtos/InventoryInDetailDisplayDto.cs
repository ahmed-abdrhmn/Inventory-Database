using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts.DisplayDtos
{
    public class InventoryInDetailDisplayDto
    {
        public int InventoryInDetailId { get; set; }
        public InventoryInHeaderDisplayDto InventoryInHeader { get; set; } = null!;
        public int Serial { get; set; }
        public ItemDisplayDto Item { get; set; } = null!;
        public PackageDisplayDto Package { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateOnly ExpireDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal ConsumerPrice { get; set; }
        public decimal TotalValue => Quantity * ConsumerPrice; //This is where we compute the TotalValue in the detailed inventory header

        public static InventoryInDetailDisplayDto FromEntity(InventoryInDetail e){
            return new InventoryInDetailDisplayDto() {
                InventoryInDetailId = e.InventoryInDetailId,
                InventoryInHeader = InventoryInHeaderDisplayDto.FromEntity(e.InventoryInHeader),
                Serial = e.Serial,
                Item = ItemDisplayDto.FromEntity(e.Item),
                Package = PackageDisplayDto.FromEntity(e.Package),
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