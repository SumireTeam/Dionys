using System;
using System.Collections.Generic;
using System.Linq;
using Dionys.Infrastructure.Models;

namespace Dionys.Infrastructure.Services
{
    public interface IUserService : ICrudService<User>
    {
        bool IsRightPassword(Guid id, string password);
        bool Lock(Guid id);
        bool IsLocked(Guid id);
        bool Unlock(Guid id);
        bool IsExist(Guid id, bool includeDeleted);
    }

    public class UserService : IUserService
    {
        private readonly DionysContext _context;

        public UserService(DionysContext context)
        {
            _context = context;
        }

        public bool Create(User user, bool ignoreValidator = false)
        {
            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(User entity, bool ignoreValidator = false)
        {


            throw new NotImplementedException();
        }

        public bool Delete(User entity, bool ignoreValidator = false)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;

            _context.Users.Update(entity);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetById(Guid id, bool includeCopmlexEntities = true)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> SearchByName(string searchParameter)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Guid id)
        {
            return IsExist(id, false);
        }

        public bool IsRightPassword(Guid id, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user != null)
            {
                if (user.PasswordHash == password)
                {
                    // TODO: Do hash
                    return true;
                }
            }

            return false;
        }

        public bool Lock(Guid id)
        {
            try
            {
                var user = _context.Users.First(u => u.Id == id);
                user.LockedAt = DateTimeOffset.UtcNow;

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsLocked(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            return user?.LockedAt != null;
        }

        public bool Unlock(Guid id)
        {
            try
            {
                var user = _context.Users.First(u => u.Id == id);

                if (!IsLocked(id))
                    return false;

                user.DeletedAt = null;
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsExist(Guid id, bool includeDeleted)
        {
            if (includeDeleted)
                return _context.Users.Any(u => u.Id == id);

            return _context.Users.Any(u => u.Id == id && u.DeletedAt == null);
        }
    }
}
