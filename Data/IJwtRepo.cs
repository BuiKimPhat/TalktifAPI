namespace TalktifAPI.Data
{
    public interface IJwtRepo
    {
        public string GenerateSecurityToken(string email);
    }
}