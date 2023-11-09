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
    public class PackageController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDbContext; //depends injection

        public PackageController(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllPackages(){
            var Packages = from i in _inventoryDbContext.Packages select PackageDto.FromEntity(i);
            return Ok(Packages);
        }

        [HttpGet]
        [Route("ById/{id:int}")]
        public IActionResult GetPackageById(int id){
            var Package = (from i in _inventoryDbContext.Packages where i.PackageId == id select PackageDto.FromEntity(i))
                            .SingleOrDefault();

            if (Package is not null){
                return Ok(Package);
            }else{
                return NotFound("Not Package with supplied Id exisits");
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeletePackage(int id){
            int result = _inventoryDbContext.Packages
                            .Where(d => d.PackageId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return Ok($"Package number {id} successfully deleted");
            }else{
                return NotFound("No Package with supplied Id was found");
            }
        }

        [HttpPost]
        [Route("New")]
        public IActionResult NewPackage(PackageCreationDto args){
            var toAdd = new Package(){
                Name = args.Name!
            };

            _inventoryDbContext.Packages.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return Ok(PackageDto.FromEntity(toAdd)); //this will include the id of the newly created Package
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdatePackage(int id, PackageCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Packages where i.PackageId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                return NotFound("No Package with supplied Id was found");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return Ok(PackageDto.FromEntity(toUpdate)); //this will include the id of the newly created Package
            }
        }               
    }
}