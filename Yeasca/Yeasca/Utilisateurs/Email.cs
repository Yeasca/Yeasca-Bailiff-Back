using System.Text.RegularExpressions;

namespace Yeasca.Metier
{
    public class Email
    {
        public Email()
        {
            Valeur = string.Empty;
        }

        public Email(string valeur)
        {
            Valeur = valeur;
        }

        public string Valeur { get; set; }

        public bool estValide()
        {
            return estRenseigné()
                   && aLaBonneLongueur()
                   && aLeBonFormat();
        }

        private bool estRenseigné()
        {
            return !string.IsNullOrEmpty(Valeur);
        }

        private bool aLaBonneLongueur()
        {
            return Valeur != null && Valeur.Length <= Ressource.Validation.LONGUEUR_MAX_EMAIL;
        }

        private bool aLeBonFormat()
        {
            if (!string.IsNullOrEmpty(Valeur))
            {
                Regex règle = new Regex(
                   string.Concat(
                    @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*",
                    "@",
                    @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"
                    )
                );
                Match comparaison = règle.Match(Valeur);
                return comparaison.Success;
            }
            return true; 
        }

        public string obtenirLErreur()
        {
            string message = string.Empty;
            if (!estRenseigné())
                return Ressource.Validation.EMAIL_REQUIS;
            if (!aLaBonneLongueur())
                return Ressource.Validation.EMAIL_LONGUEUR_MAX;
            if (!aLeBonFormat())
                return Ressource.Validation.EMAIL_INVALIDE;
            return message;
        }

        public override bool Equals(object obj)
        {
            return obj is Email 
                && this.Valeur.Equals((obj as Email).Valeur);
        }
    }
}
