﻿namespace Orders.Backend.UnitsOfWork
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<Response<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public virtual async Task<Response<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async Task<Response<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public virtual async Task<Response<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public virtual async Task<Response<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
    }
}