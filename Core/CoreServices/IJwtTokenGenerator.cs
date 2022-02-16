namespace Core.CoreServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(string nameIdentifier, string role);
    }
}