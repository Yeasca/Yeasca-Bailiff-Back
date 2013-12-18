using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class CodePostal
    {
        public CodePostal() { }

        public CodePostal(string valeur)
        {
            Code = valeur;
        }

        public string Code { get; set; }

        public bool estValide()
        {
            int valeurConvertie;
            return !estVide()
                && Code.Length == 5
                && int.TryParse(Code, out valeurConvertie);
        }

        private bool estVide()
        {
            return string.IsNullOrEmpty(Code);
        }

        public List<string> obtenirLesErreurs()
        {
            List<string> message = new List<string>();
            validerLeCodePostal(message);
            return message;
        }

        private void validerLeCodePostal(List<string> message)
        {
            if (estVide())
                message.Add(Ressource.Validation.CODE_POSTAL_REQUIS);
            else if (!estValide())
                message.Add(Ressource.Validation.CODE_POSTAL_INVALIDE);
        }

        public string enChaine()
        {
            return Code;
        }
    }
}
