namespace ChessTournament.Applications.Interfaces.Service;

public interface IService<Key, T>
{
    IAsyncEnumerable<T> GetAllAsync();
    Task<T> GetOneByIdAsync(Key key);
    Task<T> CreateAsync(T entity);
}