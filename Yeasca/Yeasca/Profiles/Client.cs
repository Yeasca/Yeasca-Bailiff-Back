
namespace Yeasca.Metier
{
    public class Client : Profile
    {
        public Client() : base()
        {
        }

        public override Erreur obtenirLesErreurs()
        {
            Erreur message = base.obtenirLesErreurs();
            if(!Adresse.estValide())
                message.ajouterUneErreur(Adresse.obtenirLesErreurs());
            return message;
        }

        public override string obtenirLaDescription()
        {
            if (nAPasLesInformationsNécessaires())
                return string.Empty;
            string description = templateDeLaDescription()
                .Replace("{Abréviation}", _chaineParAbréviation[Abréviation])
                .Replace("{Nom}", Nom)
                .Replace("{Prénom}", Prénom)
                .Replace("{DénominationSociété}", DénominationEntreprise)
                .Replace("{Adresse}", Adresse.enChaineAvecUnSéparateur("-"));
            return description;
        }

        private string templateDeLaDescription()
        {
            if(aUneEntreprise())
                return "{Abréviation} {Nom} {Prénom}, travaillant pour la société {DénominationSociété} à l'adresse {Adresse}, me déclare que";
            return
                "{Abréviation} {Nom} {Prénom}, résidant à l'adresse {Adresse}, me déclare que";
        }
    }
}
