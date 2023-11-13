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
    public class PackageController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IPackageRepo _iPackageRepo;

        public PackageController(
            IPackageRepo iPackageRepo){
                _iPackageRepo = iPackageRepo;
            }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllPackagees(){
            var result = _iPackageRepo.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPackageById(int id){
            var result = _iPackageRepo.Get(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeletePackage(int id){
            _iPackageRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewPackage(PackageCreationDto args){
            var result = _iPackageRepo.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdatePackage(int id, PackageCreationDto args){
            var result = _iPackageRepo.Update(id,args);
            return Ok(result);
        }        
    }
}