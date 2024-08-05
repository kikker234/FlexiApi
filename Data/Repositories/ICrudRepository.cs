using Data.Models;
using Data.Utils;

namespace Data.Repositories;

public interface ICrudRepository<T> where T : class
{

    bool Create(T t);
    Optional<T> Read(int id);
    IEnumerable<T> ReadAll();
    bool Update(T t);
    bool Delete(T t);
    bool Delete(int id);

}