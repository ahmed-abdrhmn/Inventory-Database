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
    public class InventoryController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IInventoryInDetailRepo _iInventoryInDetailRepo;

        public InventoryController(
            IInventoryInDetailRepo iInventoryInDetailRepo){
                _iInventoryInDetailRepo = iInventoryInDetailRepo;
            }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryInDetailes(){
            var result = _iInventoryInDetailRepo.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetInventoryInDetailById(int id){
            var result = _iInventoryInDetailRepo.Get(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteInventoryInDetail(int id){
            _iInventoryInDetailRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewInventoryInDetail(InventoryInDetailCreationDto args){
            var result = _iInventoryInDetailRepo.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateInventoryInDetail(int id, InventoryInDetailCreationDto args){
            var result = _iInventoryInDetailRepo.Update(id,args);
            return Ok(result);
        }        
    }
}