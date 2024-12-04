﻿namespace ChessTournament.Applications.Interfaces.Service;

public interface IService<Key, T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetOneByIdAsync(Key key);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}