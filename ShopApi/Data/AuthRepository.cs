using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models;

namespace ShopApi.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);

    }
    public class AuthRepository : IAuthRepository
    {
        protected IGenericUnitOfWork _uow;
        public AuthRepository(IGenericUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _uow.Repository<User>().GetByIDAsync(x => x.UserName == userName, x => x.Photos);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _uow.Repository<User>().InsertAsync(user);

            await _uow.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            var users = await _uow.Repository<User>().GetListAsync(x => x.UserName == userName);
            return users.ToList().Count() > 0;
        }
    }
}