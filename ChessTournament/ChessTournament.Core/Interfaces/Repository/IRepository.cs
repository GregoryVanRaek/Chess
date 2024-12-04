﻿namespace ChessTournament.Applications.Interfaces.Repository;

public interface IRepository<Key,T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetOneByIdAsync(Key key);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}