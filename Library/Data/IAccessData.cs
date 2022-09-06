namespace Library.Data
{
    public interface IAccessData<TData> where TData : class, new()
    {
        List<TData> Data { get; set; }
        public void UpdateFile();
    }
}