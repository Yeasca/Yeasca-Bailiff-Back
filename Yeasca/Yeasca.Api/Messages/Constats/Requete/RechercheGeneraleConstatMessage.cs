using Yeasca.Requete;

namespace Yeasca.Api
{
    public class RechercheGeneraleConstatMessage : IRechercheGeneraleConstatMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string Texte { get; set; }
    }
}