namespace SocialMedia.Infraestructure.Options
{
    public  class PasswordOptions
    {
        public int SaltSize { get; set; }
        public int KeySize { get; set; }
        public int Iteration { get; set; }
    }
}
