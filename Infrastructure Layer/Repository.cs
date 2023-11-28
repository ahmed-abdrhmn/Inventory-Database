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

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity {
        private readonly InventoryDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private static Regex fkRegex = new(@"FOREIGN KEY \(`(\w+)Id");
        private static string? ExtractForeignKey(string message){
            var match = fkRegex.Match(message);
            if (match.Success){ //should always be successful but still I am checking this.
                return match.Groups[1].Value; //Groups[0] is the whole matched string, Groups[1] is the sub string that we want
            }else{
                return null;
            }
        }
        
        public Repository(InventoryDbContext dbContext){
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<T>();
        }
        
        public T Create(T entity)
        {
            try{
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                return entity;
            }catch(DbUpdateException e){
                var b = e.GetBaseException() as MySqlException;
                if (b is not null && b.ErrorCode ==  MySqlErrorCode.NoReferencedRow2){
                    var fkey = ExtractForeignKey(b.Message);
                    if (fkey is not null){
                        throw new IDNotFoundException(fkey);
                    }else{
                        throw;
                    }
                }else{
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            var result = _dbSet.Where(x => x.Id == id).ExecuteDelete();

            if (result == 0){
                throw new IDNotFoundException(typeof(T).Name);
            }
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            try{
                return _dbSet.Where(x => x.Id == id).Single();
            }catch (InvalidOperationException){
                throw new IDNotFoundException(typeof(T).Name);
            }
        }

        public T Update(T entity)
        {
            try{
                _dbSet.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }catch (DbUpdateConcurrencyException){
                throw new IDNotFoundException(typeof(T).Name);
            }catch(DbUpdateException e){
                var b = e.GetBaseException() as MySqlException;
                if (b is not null && b.ErrorCode ==  MySqlErrorCode.NoReferencedRow2){
                    var fkey = ExtractForeignKey(b.Message);
                    if (fkey is not null){
                        throw new IDNotFoundException(fkey);
                    }else{
                        throw;
                    }
                }else{
                    throw;
                }
            }
        }
    }
}