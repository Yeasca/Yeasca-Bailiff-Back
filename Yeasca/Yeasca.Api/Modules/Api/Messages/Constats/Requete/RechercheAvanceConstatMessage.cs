using System;
using Yeasca.Requete;

namespace Yeasca.Web.Api
{
    public class RechercheAvanceConstatMessage : IRechercheAvanceConstatMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string NomClient { get; set; }
        public DateTime? DateDébut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}