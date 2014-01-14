using Yeasca.Requete;

namespace Yeasca.Api
{
    public class AuthentifierMessage : IAuthentificationMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
    }
}