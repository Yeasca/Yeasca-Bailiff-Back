using Yeasca.Requete;

namespace Yeasca.Web.Api
{
    public class RechercheUtilisateurMessage : IRechercheUtilisateurMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string NomUtilisateur { get; set; }
        public int Type { get; set; }
    }
}