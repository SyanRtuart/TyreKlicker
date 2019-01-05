namespace TyreKlicker.API.Models.Token
{
    public class RefreshToken
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}