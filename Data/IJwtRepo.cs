namespace TalktifAPI.Data
{
    public interface IJwtRepo
    {
        public string GenerateSecurityToken(int id);
        public string GenerateRefreshToken(int id);
    }
}