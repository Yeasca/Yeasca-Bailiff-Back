
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public abstract  class PartieAvecSociete : Partie
    {
        public PartieAvecSociete() : base()
        {
            Société = new Societe();
        }

        public PartieAvecSociete(string nom, string prénom, string dénominationSociété) : base(nom, prénom)
        {
            Société = new Societe();
            Société.Dénomination = dénominationSociété;
        }

        public Societe Société { get; set; }

        public override bool estValide()
        {
            return base.estValide()
                && Société.estValide();
        }

        public override List<string> obtenirLesErreurs()
        {
            List<string> message = base.obtenirLesErreurs();
            if(!Société.estValide())
                message.AddRange(Société.obtenirLesErreurs());
            return message;
        }

        protected bool nAPasLesInformationsNécessaires()
        {
            return string.IsNullOrEmpty(Nom)
                || string.IsNullOrEmpty(Prénom)
                || string.IsNullOrEmpty(Société.Adresse.enChaine());
        }
    }
}
