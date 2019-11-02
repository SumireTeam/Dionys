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

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;

            _context.Users.Update(entity);

            _context.SaveChanges();
        }

        public User GetById(Guid id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == id);
        }

        public User GetByIdOr(Guid id, IDbModel entity)
        {
            throw new NotImplementedException();
        }

        public User GetByIdOrDefault(Guid id)
        {
            throw new NotImplementedException();
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
