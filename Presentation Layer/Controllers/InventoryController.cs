using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IDocumentService _documentService;

        public InventoryController(IDocumentService documentService){
            this._documentService = documentService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryDocuments(){
            var results = _documentService.GetAll();
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetInventoryDocumentsById(int id){
            var result = _documentService.GetById(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteInventoryDocument(int id){
            _documentService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewInventoryDocument(InventoryInHeader args){
            var result = _documentService.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateInventoryDocument(int id, InventoryInHeader args){
            args.Id = id;
            var result = _documentService.Update(args);
            return Ok(result);
        }        
    }
}