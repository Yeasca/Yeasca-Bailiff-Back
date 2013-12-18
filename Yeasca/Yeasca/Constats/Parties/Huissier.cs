
namespace Yeasca.Metier
{
    public class Huissier : PartieAvecSociete
    {
        public Huissier()
            : base()
        {

        }

        public Huissier(string nom, string prénom, string dénominationCabinet) :base(nom, prénom, dénominationCabinet)
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
                "Je, {Abréviation} {Nom} {Prénom},\nHuissier de justice du cabinet {DénominationSociété} domicilié à l'adresse {Adresse}";
        }
    }
}
