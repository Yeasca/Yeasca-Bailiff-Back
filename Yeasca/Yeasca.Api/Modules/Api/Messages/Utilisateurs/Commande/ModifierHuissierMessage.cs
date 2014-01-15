using Yeasca.Commande;

namespace Yeasca.Web.Api
{
    public class ModifierHuissierMessage : IModifierHuissierMessage
    {
        public string IdHuissier { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}