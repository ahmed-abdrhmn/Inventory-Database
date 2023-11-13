using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts.DisplayDtos
{
    public class ItemDisplayDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = null!;

        public static ItemDisplayDto FromEntity(Item e){
            return new ItemDisplayDto(){
                ItemId = e.ItemId,
                Name = e.Name
            };
        }
    }
}