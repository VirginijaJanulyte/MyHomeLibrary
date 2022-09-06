using Library.Models;

namespace Library.Service
{
    public interface IBasicService<TData> where TData : BasicModel, new()
    {
        void Add(TData data);
        void Delete(int id);
        List<TData> Get();
        TData GetById(int id);
        TData GetByName(string name);
        void Update(TData data);
    }
}