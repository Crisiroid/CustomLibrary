namespace CustomLibrary.Interfaces
{
    public interface IAuthService
    {
        public string HashPassword(string password);
        public string GenerateToken(string hashedPassword, string deviceId, string ipAddress);
        public void StoreToken(string userId, string token, string deviceId, string ipAddress);
        public bool ValidateToken(string userId, string token, string deviceId, string ipAddress);
    }
}
