using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.CreationDtos;
using Presentation.DisplayDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IService<InventoryInDetail> _iids;

        public InventoryController(IService<InventoryInDetail> inventoryInDetailService){
            this._iids = inventoryInDetailService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryInDetailes(){
            var results = from i in _iids.GetAll() select InventoryInDetailDisplayDto.FromEntity(i);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetInventoryInDetailById(int id){
            var result = _iids.Get(id);
            return Ok( InventoryInDetailDisplayDto.FromEntity(result) );
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteInventoryInDetail(int id){
            _iids.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewInventoryInDetail(InventoryInDetailCreationDto args){
            InventoryInDetail entity = new() {
                InventoryInHeaderId = (int)args.InventoryInHeaderId!,
                Serial = (int)args.Serial!,
                ItemId = (int)args.ItemId!,
                PackageId = (int)args.PackageId!,
                BatchNumber = args.BatchNumber!,
                SerialNumber = args.SerialNumber!,
                ExpireDate = (DateOnly)args.ExpireDate!,
                Quantity = (decimal)args.Quantity!,
                ConsumerPrice = (decimal)args.ConsumerPrice!,
            };

            var result = _iids.Create(entity);
            return Ok(InventoryInDetailDisplayDto.FromEntity(result));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateInventoryInDetail(int id, InventoryInDetailCreationDto args){
            InventoryInDetail entity = new() {
                Id = id,
                InventoryInHeaderId = (int)args.InventoryInHeaderId!,
                Serial = (int)args.Serial!,
                ItemId = (int)args.ItemId!,
                PackageId = (int)args.PackageId!,
                BatchNumber = args.BatchNumber!,
                SerialNumber = args.SerialNumber!,
                ExpireDate = (DateOnly)args.ExpireDate!,
                Quantity = (decimal)args.Quantity!,
                ConsumerPrice = (decimal)args.ConsumerPrice!,
            };

            var result = _iids.Update(entity);
            return Ok(InventoryInDetailDisplayDto.FromEntity(result));
        }        
    }
}