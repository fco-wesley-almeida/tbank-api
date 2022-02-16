namespace Core.CoreServices
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        string Decode(string password);
    }
}