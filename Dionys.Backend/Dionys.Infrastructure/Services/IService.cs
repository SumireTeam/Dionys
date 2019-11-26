using System;
using System.Collections.Generic;
using Dionys.Infrastructure.Models;

namespace Dionys.Infrastructure.Services
{
    public interface IService
    {
    }

    public interface ICrudService<T> where T : IDbModel
    {
        void Create(T entity);
        //bool TryCreate(T entity, bool ignoreValidator = false);
        void Update(T entity);
        //bool TryUpdate(T entity, bool ignoreValidator = false);
        void Delete(T entity);
        //bool TryDelete(T entity, bool ignoreValidator = false);
        T GetById(Guid id);
        T GetByIdOr(Guid id, IDbModel entity);
        T GetByIdOrDefault(Guid id);
        IEnumerable<T> SearchByName(string searchParameter);
        bool IsExist(Guid id);
    }
}
