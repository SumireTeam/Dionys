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
        bool Create(T entity, bool ignoreValidator = false);
        bool Update(T entity, bool ignoreValidator = false);
        bool Delete(T entity, bool ignoreValidator = false);
        T GetById(Guid id, bool includeCopmlexEntities = true);
        IEnumerable<T> SearchByName(string searchParameter);
        bool IsExist(Guid id);
    }
}
