using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Infrastructure.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Domain.Exceptions;
using MySqlConnector;
using System.Text.RegularExpressions;
using System.Data;

namespace Infrastructure
{
    public class DocumentRepository : IRepository<InventoryInHeader> {
        private readonly InventoryDbContext _dbContext;

        //This function is used to update an entity if it exists otherwise insert a new one.
        //It returns a reference to the inserted/updated entity
        private T Merge<T>(T entity) where T:BaseEntity {
            var entity_in_db = _dbContext.Set<T>().Where(x => x.Id == entity.Id).SingleOrDefault();
            if(entity_in_db is not null){
                _dbContext.Entry(entity_in_db).CurrentValues.SetValues(entity);
                return entity_in_db;
            }else{
                _dbContext.Add(entity);
                return entity;
            }
        }

        public DocumentRepository(InventoryDbContext dbContext){
            this._dbContext = dbContext;
        }

        public InventoryInHeader Create(InventoryInHeader entity)
        {
            entity.Id = default; //<-- Zero make EF Core to construct a new record in the database!
            foreach(var i in entity.InventoryInDetails){ //<-- Since InventoryInDetail is a child of Headers, we must add new records of these.
                i.Id = default;
            }

            entity.Branch = Merge(entity.Branch);
            foreach(var i in entity.InventoryInDetails){
                i.Item = Merge(i.Item);
                i.Package = Merge(i.Package);
            }
            
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var result = _dbContext.InventoryInHeaders.Where(x => x.Id == id).ExecuteDelete();
            if (result == 0){
                throw new IDNotFoundException("InventoryInHeader");
            }
        }

        public IEnumerable<InventoryInHeader> GetAll()
        {
            return (from i in _dbContext.InventoryInHeaders
                    .Include("InventoryInDetails")
                        .Include("InventoryInDetails.Item")
                        .Include("InventoryInDetails.Package")
                    .Include("Branch")
                    select i);
        }

        public InventoryInHeader GetById(int id)
        {
            try{
                return (from i in _dbContext.InventoryInHeaders
                        .Include("InventoryInDetails")
                            .Include("InventoryInDetails.Item")
                            .Include("InventoryInDetails.Package")
                        .Include("Branch")
                        where i.Id == id
                        select i
                ).Single();
            }catch(InvalidOperationException){
                throw new IDNotFoundException("InventoryInHeader");
            }
        }

        public InventoryInHeader Update(InventoryInHeader entity)
        {
            var current = (from i in _dbContext.InventoryInHeaders
                        .Include("InventoryInDetails")
                            .Include("InventoryInDetails.Item")
                            .Include("InventoryInDetails.Package")
                        .Include("Branch")
                        where i.Id == entity.Id
                        select i
                ).Single();

            current.Branch = Merge(entity.Branch);
            List<InventoryInDetail> modified = new();

            foreach(var i in entity.InventoryInDetails){
                //check if the supplied id of InventoryInDetail exists in the database already
                var c_id = (from j in current.InventoryInDetails where j.Id == i.Id select j).SingleOrDefault();
                if (c_id is null){
                    i.Id = 0; //Add the InventoryInDetail if it is not already in this particular header...
                }

                i.Item = Merge(i.Item);
                i.Package = Merge(i.Package);

                modified.Add(Merge(i));
            }
            
            
            //Delete the inventoryInDetail entities that are no longer in the array if this header
            //This prevents SQL from complaining about null foreign keys.
            foreach(var i in current.InventoryInDetails){
                var d_id = (from j in modified where j.Id == i.Id select j).SingleOrDefault();
                if (d_id is null){
                    _dbContext.Remove(i);
                }
            }
            
            current.InventoryInDetails = modified;

            _dbContext.SaveChanges();
            return entity;
        }
    }
}