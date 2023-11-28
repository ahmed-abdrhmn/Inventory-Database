using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IService<T>
    {
        public IEnumerable<T> GetAll();
        public T Get(int id);
        public T Create(T t);
        public void Delete(int id);
        public T Update(T t);
    }
}