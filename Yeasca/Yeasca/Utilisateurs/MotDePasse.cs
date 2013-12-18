
using System.Text.RegularExpressions;

namespace Yeasca.Metier
{
    public class MotDePasse
    {
        private readonly ChiffrementAES _chiffrement = new ChiffrementAES();

        public MotDePasse() { }

        public MotDePasse(string valeurDéchiffrée)
        {
            ValeurDéchiffrée = valeurDéchiffrée;
        }

        public byte [] Valeur { get; set; }

        public string ValeurDéchiffrée
        {
            get
            {
                if(aUnMotDePasse())
                    return _chiffrement.décrypter(Valeur);
                return string.Empty;
            }
            set
            {
                Valeur = _chiffrement.crypter(value);
            }
        }

        private bool aUnMotDePasse()
        {
            return Valeur != null && Valeur.Length > 0;
        }

        private bool aUnMotDePasseAssezLong()
        {
            return Valeur != null && ValeurDéchiffrée.Length >= Ressource.Validation.LONGUEUR_MIN_MOT_DE_PASSE;
        }

        private bool aUnMotDePasseDeLaBonneLongueur()
        {
            return Valeur != null && ValeurDéchiffrée.Length <= Ressource.Validation.LONGUEUR_MAX_MOT_DE_PASSE;
        }

        private bool aUnMotDePasseComplexe()
        {
            if (aUnMotDePasse())
            {
                Regex règle = new Regex(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$");
                Match comparaison = règle.Match(ValeurDéchiffrée);
                return comparaison.Success;
            }
            return false; 
        }

        public bool estValide()
        {
            return aUnMotDePasse()
                && aUnMotDePasseAssezLong()
                && aUnMotDePasseDeLaBonneLongueur()
                && aUnMotDePasseComplexe();
        }

        public string obtenirLErreur()
        {
            string message = string.Empty;
            if (!aUnMotDePasse())
                return Ressource.Validation.MOT_DE_PASSE_REQUIS;
            if (!aUnMotDePasseAssezLong())
                return Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MIN;
            if (!aUnMotDePasseDeLaBonneLongueur())
                return Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MAX;
            if (!aUnMotDePasseComplexe())
                return Ressource.Validation.MOT_DE_PASSE_COMPLEXE;
            return message;
        }
    }
}
