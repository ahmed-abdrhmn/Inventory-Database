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
    public class HeaderController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IInventoryInHeaderRepo _iInventoryInHeaderRepo;

        public HeaderController(
            IInventoryInHeaderRepo iInventoryInHeaderRepo){
                _iInventoryInHeaderRepo = iInventoryInHeaderRepo;
            }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryInHeaderes(){
            var result = _iInventoryInHeaderRepo.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetInventoryInHeaderById(int id){
            var result = _iInventoryInHeaderRepo.Get(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteInventoryInHeader(int id){
            _iInventoryInHeaderRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewInventoryInHeader(InventoryInHeaderCreationDto args){
            var result = _iInventoryInHeaderRepo.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateInventoryInHeader(int id, InventoryInHeaderCreationDto args){
            var result = _iInventoryInHeaderRepo.Update(id,args);
            return Ok(result);
        }        
    }
}