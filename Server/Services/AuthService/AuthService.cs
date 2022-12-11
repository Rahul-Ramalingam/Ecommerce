using System.Security.Cryptography;

namespace Ecommerce.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;

        public AuthService(DataContext context)
        {
            _dbContext = context;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success= false,
                    Message= "User already exists.."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Success = true,
                Data = user.Id
            };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _dbContext.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
