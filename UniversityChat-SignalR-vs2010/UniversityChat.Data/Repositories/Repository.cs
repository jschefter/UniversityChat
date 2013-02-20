using System.Collections.Generic;

namespace UniversityChat.Data.Repositories
{
    public interface Repository<T>
    {
        bool Create(T item);

        bool Delete(T item);

        IList<T> GetAll();

        T GetById(decimal id);

        T GetByCriteria(T item);

        bool Update(T item);
    }
}
