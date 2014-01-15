using Yeasca.Commande;

namespace Yeasca.Web.Api
{
    public class ConnexionMessage : IConnexionMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
    }
}