namespace Repository;
using Domain.Models;

//The repository will only deal with InventoryInHeader since it is the master domain model
public interface IRepository<T> where T: AggregateRoot{
    public IEnumerable<InventoryInHeader> GetAll();
    public InventoryInHeader GetById(int id);
    public void Delete(int id);
    public InventoryInHeader Create(InventoryInHeader entity);
    public InventoryInHeader Update(InventoryInHeader entity);
}