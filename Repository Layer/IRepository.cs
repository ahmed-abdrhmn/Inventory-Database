namespace Repository;
using Domain.Models;


public interface IRepository<T> where T: BaseEntity {
    public List<T> GetAll();
    public T GetById(int id);
    public void Delete(int id);
    public T Create(T entity);
    public T Update(T entity);
}