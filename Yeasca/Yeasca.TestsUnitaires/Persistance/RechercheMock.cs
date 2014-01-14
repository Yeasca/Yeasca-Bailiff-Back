using System;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Persistance
{
    public class RechercheConstatTest : IRechercheConstat
    {
        public RechercheConstatTest()
        {
            NuméroPage = 1;
            NombreDElémentsParPage = 10;
        }

        public string NomClient { get; set; }
        public DateTime? DateDébut { get; set; }
        public DateTime? DateFin { get; set; }
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
    }

    public class RechercheGlobaleTest : IRechercheGlobale
    {
        public RechercheGlobaleTest()
        {
            NuméroPage = 1;
            NombreDElémentsParPage = 10;
        }

        public string Texte { get; set; }
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
    }

    public class RechercheUtilisateurTest : IRechercheUtilisateur
    {
        public RechercheUtilisateurTest()
        {
            NuméroPage = 1;
            NombreDElémentsParPage = 10;
            Type = (int)TypeUtilisateur.Inconnu;
        }

        public string NomUtilisateur { get; set; }
        public int Type { get; set; }
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
    }
}
