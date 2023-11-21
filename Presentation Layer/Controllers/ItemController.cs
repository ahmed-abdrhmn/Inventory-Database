using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
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
        private readonly IItemRepo _iItemRepo;

        public ItemController(
            IItemRepo iItemRepo){
                _iItemRepo = iItemRepo;
            }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllItemes(){
            var result = _iItemRepo.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetItemById(int id){
            var result = _iItemRepo.Get(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteItem(int id){
            _iItemRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewItem(ItemCreationDto args){
            var result = _iItemRepo.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateItem(int id, ItemCreationDto args){
            var result = _iItemRepo.Update(id,args);
            return Ok(result);
        }        
    }
}