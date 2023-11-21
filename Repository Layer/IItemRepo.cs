using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;

namespace Services
{
    public interface IItemRepo
    {
        public ItemDisplayDto Get(int id);
        public IEnumerable<ItemDisplayDto> GetAll();
        public ItemDisplayDto Create(ItemCreationDto args);
        public void Delete(int id);
        public ItemDisplayDto Update(int id, ItemCreationDto args);           
    }
}