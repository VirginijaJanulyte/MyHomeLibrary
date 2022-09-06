using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class BasicService<TData> : IBasicService<TData> where TData : BasicModel, new()
    {
        private IAccessData<TData> _data;
        public BasicService(IAccessData<TData> data)
        {
            _data = data;           
        }
        public List<TData> Get()
        {
            return _data.Data.OrderBy(c => c.Name).ToList();
        }
        public TData GetById(int id)
        {
            var data = _data.Data;
            return data.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
        }
        public TData GetByName(string name)
        {
            var data = _data.Data;
            return data.Where(c => c.Name.ToLower() == name.ToLower()).Select(c => c).FirstOrDefault();
        }
        public virtual void Add(TData data)
        {
            if (string.IsNullOrEmpty(data.Name))
            {
                throw new Exception("Can't add. The name is required.");
            }
            if (GetByName(data.Name) != null||data.Name.Count()>15)
            {
                throw new Exception("Can't add.");
            }           
            data.Id = CreateId();
            _data.Data.Add(data);
            _data.UpdateFile();            
        }
        public virtual void Update(TData data)
        {
            if (data == null)
            {
                throw new Exception($"Can't update.");
            }
            if (String.IsNullOrEmpty(data.Name) || data.Name.Count() > 15 || data.Id == 0)
            {
                throw new Exception($"Can't update.");
            }
            TData dataToUpdate = _data.Data.Where(c => c.Id == data.Id).Select(c => c).FirstOrDefault();
            if (dataToUpdate == null)
            {
                throw new Exception($"Can't find Id {data.Id}.");
            }           
            dataToUpdate.Name=data.Name;
            _data.UpdateFile();
        }
        public virtual void Delete(int id)
        {          
            var dataToDelete = _data.Data.Where(c => c.Id == id).Select(c => c).FirstOrDefault();
            if (dataToDelete == null)
            {
                throw new Exception($"Can't delete. Id {id} doesn't exist.");              
            }
            _data.Data.Remove(dataToDelete);
            _data.UpdateFile();
        }
        protected int CreateId()
        {
            var data = _data.Data;
            if (data.Count == 0)
            {
                return 1;
            }
            return data.Select(b => b.Id).Max() + 1;
        }
    }
}
