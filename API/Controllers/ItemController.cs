using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models.CreationDtos;
using DataAccess.Data;
using DataAccess.Models;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDbContext; //depends injection

        public ItemController(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllItems(){
            var Items = from i in _inventoryDbContext.Items select ItemDto.FromEntity(i);
            return Ok(Items);
        }

        [HttpGet]
        [Route("ById/{id:int}")]
        public IActionResult GetItemById(int id){
            var Item = (from i in _inventoryDbContext.Items where i.ItemId == id select ItemDto.FromEntity(i))
                            .SingleOrDefault();

            if (Item is not null){
                return Ok(Item);
            }else{
                return NotFound("Not Item with supplied Id exisits");
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeleteItem(int id){
            int result = _inventoryDbContext.Items
                            .Where(d => d.ItemId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return Ok($"Item number {id} successfully deleted");
            }else{
                return NotFound("No Item with supplied Id was found");
            }
        }

        [HttpPost]
        [Route("New")]
        public IActionResult NewItem(ItemCreationDto args){
            var toAdd = new Item(){
                Name = args.Name!
            };

            _inventoryDbContext.Items.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return Ok(ItemDto.FromEntity(toAdd)); //this will include the id of the newly created Item
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdateItem(int id, ItemCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Items where i.ItemId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                return NotFound("No Item with supplied Id was found");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return Ok(ItemDto.FromEntity(toUpdate)); //this will include the id of the newly created Item
            }
        }               
    }
}