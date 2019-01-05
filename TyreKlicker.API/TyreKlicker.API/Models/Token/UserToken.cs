using System;

namespace TyreKlicker.API.Models.Token
{
    public class UserToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}