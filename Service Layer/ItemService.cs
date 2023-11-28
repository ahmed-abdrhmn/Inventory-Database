using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Domain.Models;
using Repository;

namespace Services
{
    public class ItemService: IService<Item>
    {
        private readonly IRepository<Item> _item_repo;

        public ItemService(
            IRepository<Item> item
        ){
            this._item_repo = item;
        }

        public IEnumerable<Item> GetAll(){
            return _item_repo.GetAll();;
        }

        public Item Get(int id){
            return _item_repo.GetById(id);
        }

        public Item Create(Item item){
            //I will assume args has all it fields set
            return _item_repo.Create(item);
        }

        public void Delete(int id){
            _item_repo.Delete(id);
        }

        public Item Update(Item item){
            return _item_repo.Update(item);
        }      
    }
}