using CustomLibrary.Data;
using CustomLibrary.Interfaces;
using CustomLibrary.Models;
using System.Security.Cryptography;
using System.Text;

namespace CustomLibrary.Services
{
    public class AuthService : IAuthService
    {
        private readonly LibraryDBContext _dbContext; 

        public AuthService(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GenerateToken(string hashedPassword, string deviceId, string ipAddress)
        {
            var key = Encoding.UTF8.GetBytes("HelloThisIsAmir");
            using (var hmac = new HMACSHA256(key))
            {
                var data = $"{hashedPassword}:{deviceId}:{ipAddress}";
                var token = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(token);
            }
        }

        public string HashPassword(string password)
        {
            var fixedSalt = Encoding.UTF8.GetBytes("ThisisALibrary");
            using(var rfc2898 = new Rfc2898DeriveBytes(password, fixedSalt, 10000, HashAlgorithmName.SHA256))
            {
                var hash = rfc2898.GetBytes(32);
                return Convert.ToBase64String(fixedSalt) + ":" + Convert.ToBase64String(hash);
            }
        }

        public void StoreToken(string userId, string token, string deviceId, string ipAddress)
        {
            var tokenRecord = new Token
            {
                userId = userId,
                deviceId = deviceId,
                ipAddress = ipAddress,
                token = token,
                createdDate = DateTime.Now
            };
            _dbContext.Tokens.Add(tokenRecord);
            _dbContext.SaveChanges();
        }

        public bool ValidateToken(string userId, string token, string deviceId, string ipAddress)
        {
            var registeredToken = _dbContext.Tokens.SingleOrDefault( t => t.userId == userId && t.ipAddress == ipAddress && t.deviceId == deviceId &&  t.token == token);
            if(registeredToken == null)
            {
                return false; 
            }

            return registeredToken.token == token; 
        }
    }
}
