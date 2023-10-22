namespace CatBoyCommon;

public interface IRepository<T>
{
    bool Add(ref T obj);
    bool AddAll(IEnumerable<T> obj);
    bool Update(T obj);
    bool Delete(T obj);
    IEnumerable<T> GetAll();
    T GetById(int id);
}