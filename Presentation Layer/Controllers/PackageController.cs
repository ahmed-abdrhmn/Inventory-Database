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
    public class PackageController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IService<Package> _packageService;

        public PackageController(
        IService<Package> packageService){
            this._packageService = packageService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllPackages(){
            var results = from i in _packageService.GetAll() select PackageDisplayDto.FromEntity(i);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPackageById(int id){
            var result = _packageService.Get(id);
            return Ok(PackageDisplayDto.FromEntity(result));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeletePackage(int id){
            _packageService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewPackage(PackageCreationDto args){
            Package entity = new() {
                Name = args.Name!
            };

            var result = _packageService.Create(entity);
            return Ok(PackageDisplayDto.FromEntity(result));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdatePackage(int id, PackageCreationDto args){
            Package entity = new() {
                Id = id,
                Name = args.Name!
            };

            var result = _packageService.Update(entity);
            return Ok(PackageDisplayDto.FromEntity(result));
        }        
    }
}