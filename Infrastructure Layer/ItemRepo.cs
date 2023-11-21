using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Data;
using Contracts.DisplayDtos;
using Microsoft.EntityFrameworkCore;
using Contracts.Exceptions;
using Contracts.CreationDtos;

namespace Services.Impl
{
    public class ItemRepo: IItemRepo
    {
        private readonly InventoryDbContext _inventoryDbContext;
        
        public ItemRepo(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }
        
        public IEnumerable<ItemDisplayDto> GetAll(){
            var Items = from i in _inventoryDbContext.Items select ItemDisplayDto.FromEntity(i);
            return Items;            
        }
        public ItemDisplayDto Get(int id){
            var Item = (from i in _inventoryDbContext.Items where i.ItemId == id select ItemDisplayDto.FromEntity(i))
                            .SingleOrDefault();

            if (Item is not null){
                return Item;
            }else{
                throw new IDNotFoundException("Item");
            }            
        }
        public void Delete(int id){
            int result = _inventoryDbContext.Items
                            .Where(d => d.ItemId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return; //success
            }else{
                throw new IDNotFoundException("Item");
            }            
        }
        public ItemDisplayDto Create(ItemCreationDto args){
            var toAdd = new Item(){
                Name = args.Name!
            };

            _inventoryDbContext.Items.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return ItemDisplayDto.FromEntity(toAdd); //this will include the id of the newly created Item            
        }
        public ItemDisplayDto Update(int id, ItemCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Items where i.ItemId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                throw new IDNotFoundException("Item");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return ItemDisplayDto.FromEntity(toUpdate); //this will include the id of the newly created Item
            }            
        }        
    }
}