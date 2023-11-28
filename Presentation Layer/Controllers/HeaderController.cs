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
    public class HeaderController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IService<InventoryInHeader> _iihs;

        public HeaderController(IService<InventoryInHeader> inventoryInHeaderService){
            this._iihs = inventoryInHeaderService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryInHeaderes(){
            var results = from i in _iihs.GetAll() select InventoryInHeaderDisplayDto.FromEntity(i);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetInventoryInHeaderById(int id){
            var result = _iihs.Get(id);
            return Ok( InventoryInHeaderDisplayDto.FromEntity(result) );
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteInventoryInHeader(int id){
            _iihs.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewInventoryInHeader(InventoryInHeaderCreationDto args){
            InventoryInHeader entity = new() {
                BranchId = (int)args.BranchId!,
                DocDate = (DateOnly)args.DocDate!,
                Reference = args.Reference!,
                Remarks = args.Remarks!
            };

            var result = _iihs.Create(entity);
            return Ok(InventoryInHeaderDisplayDto.FromEntity(result));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateInventoryInHeader(int id, InventoryInHeaderCreationDto args){
            InventoryInHeader entity = new() {
                Id = id,
                BranchId = (int)args.BranchId!,
                DocDate = (DateOnly)args.DocDate!,
                Reference = args.Reference!,
                Remarks = args.Remarks!
            };

            var result = _iihs.Update(entity);
            return Ok(InventoryInHeaderDisplayDto.FromEntity(result));
        }        
    }
}