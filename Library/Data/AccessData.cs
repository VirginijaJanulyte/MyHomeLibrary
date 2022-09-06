using System.Text.Json;
using System.Xml.Linq;

namespace Library.Data
{
    public class AccessData<TData> : IAccessData<TData> where TData : class, new()
    {
        
        private string _fileName;
        public List<TData> Data { get; set; } = new List<TData>();

        public AccessData(string fileName)
        {
            var dir = Directory.CreateDirectory($"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}LibraryData");
            _fileName = $"{dir}\\{fileName}.json";
            Load();
        }
        private void Save()
        {
            try
            {
                var options = new JsonSerializerOptions() { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(Data, options);
                File.WriteAllText(_fileName, jsonString);
            }
            catch
            {
                throw new Exception();
            }
        }
        private void Load()
        {
            try
            {
                if (File.Exists(_fileName))
                {
                    string jsonString = File.ReadAllText(_fileName);
                    Data = JsonSerializer.Deserialize<List<TData>>(jsonString);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public void UpdateFile()
        {
            Save();
            Load();
        }
    }
}
