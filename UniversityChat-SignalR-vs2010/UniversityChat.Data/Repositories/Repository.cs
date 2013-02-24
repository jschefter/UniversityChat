using System.Collections.Generic;

namespace UniversityChat.Data.Repositories
{
    public interface IRepository<T>
    {
        bool Create(T item);

        bool Delete(T item);

        IList<T> GetAll();

        T GetById(decimal id);

        T GetByCriteria(string criteriaName, T item);

        bool Update(T item);
    }
}
