
namespace Yeasca.Metier
{
    public class ClientParticulier : Partie
    {
        public ClientParticulier() : base()
        {
        }

        public Adresse Adresse { get; set; }

        public override string obtenirLaDescription()
        {
            if (nAPasLesInformationsNécessaires())
                return string.Empty;
            string description = templateDeLaDescription()
                .Replace("{Abréviation}", _chaineParAbréviation[Abréviation])
                .Replace("{Nom}", Nom)
                .Replace("{Prénom}", Prénom)
                .Replace("{Adresse}", Adresse.enChaineAvecUnSéparateur("-"));
            return description;
        }

        private bool nAPasLesInformationsNécessaires()
        {
            return string.IsNullOrEmpty(Nom)
                || string.IsNullOrEmpty(Prénom)
                || string.IsNullOrEmpty(Adresse.enChaine());
        }

        private string templateDeLaDescription()
        {
            return
                "{Abréviation} {Nom} {Prénom}, résidant à l'adresse {Adresse}, me déclare que";
        }
    }
}
