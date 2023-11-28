using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.CreationDtos;
using Presentation.DisplayDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IService<Item> _itemService;

        public ItemController(
        IService<Item> itemService){
            this._itemService = itemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllItemes(){
            var results = from i in _itemService.GetAll() select ItemDisplayDto.FromEntity(i);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetItemById(int id){
            var result = _itemService.Get(id);
            return Ok(ItemDisplayDto.FromEntity(result));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteItem(int id){
            _itemService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewItem(ItemCreationDto args){
            Item entity = new() {
                Name = args.Name!
            };

            var result = _itemService.Create(entity);
            return Ok(ItemDisplayDto.FromEntity(result));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateItem(int id, ItemCreationDto args){
            Item entity = new() {
                Id = id,
                Name = args.Name!
            };

            var result = _itemService.Update(entity);
            return Ok(ItemDisplayDto.FromEntity(result));
        }        
    }
}