namespace Services;
using Domain.Models;

public interface IDocumentService
{
    public IEnumerable<InventoryInHeader> GetAll();
    public InventoryInHeader GetById(int id);
    public void Delete(int id);
    public InventoryInHeader Create(InventoryInHeader arg);
    public InventoryInHeader Update (InventoryInHeader arg);
}
