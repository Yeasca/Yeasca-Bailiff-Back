
namespace Yeasca.Metier
{
    public class ClientSociete : PartieAvecSociete
    {
        public ClientSociete() : base()
        {
            
        }

        public override string obtenirLaDescription()
        {
            if (nAPasLesInformationsNécessaires())
                return string.Empty;
            string description = templateDeLaDescription()
                .Replace("{Abréviation}", _chaineParAbréviation[Abréviation])
                .Replace("{Nom}", Nom)
                .Replace("{Prénom}", Prénom)
                .Replace("{DénominationSociété}", Société.Dénomination)
                .Replace("{Adresse}", Société.Adresse.enChaineAvecUnSéparateur("-"));
            return description;
        }

        private string templateDeLaDescription()
        {
            return
                "{Abréviation} {Nom} {Prénom}, travaillant pour l'entreprise {DénominationSociété} à l'adresse {Adresse}, me déclare que";
        }
    }
}
