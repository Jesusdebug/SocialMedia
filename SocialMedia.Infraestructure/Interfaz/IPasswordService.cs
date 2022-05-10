namespace SocialMedia.Infraestructure.Interfaz 
{ 
    public interface IPasswordService
    {
        string Hash(string password);
        bool Check(string hash, string password);
    }
}
