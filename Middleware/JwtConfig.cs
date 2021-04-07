namespace TalktifAPI.Middleware
{
    public class JwtConfig
    {
        public string secret { get; set; }
        public int expirationInHours { get; set; }
        public string Issuer { get; set; }
        public string Audiences { get; set; }
        
        
        
    }
}