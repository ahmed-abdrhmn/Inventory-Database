using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace API.Models
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = null!;

        public static ItemDto FromEntity(Item e){
            return new ItemDto(){
                ItemId = e.ItemId,
                Name = e.Name
            };
        }
    }
}