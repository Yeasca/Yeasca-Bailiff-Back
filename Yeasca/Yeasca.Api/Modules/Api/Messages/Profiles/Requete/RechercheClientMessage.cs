using Yeasca.Requete;

namespace Yeasca.Web.Api
{
    public class RechercheClientMessage : IRechercheClientMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string Texte { get; set; }
    }
}