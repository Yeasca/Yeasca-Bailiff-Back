
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Erreur
    {
        private List<string> _listeDesErreurs = new List<string>();

        public void ajouterUneErreur(string erreur)
        {
            _listeDesErreurs.Add(erreur);
        }

        public int Nombre
        {
            get
            {
                return _listeDesErreurs.Count;
            }
        }

        public void ajouterUneErreur(Erreur erreur)
        {
            _listeDesErreurs.AddRange(erreur._listeDesErreurs);
        }

        public List<string> donnerLaListeDesErreurs()
        {
            return _listeDesErreurs;
        }

        public string donnerLaListeEnHTML()
        {
            if (Nombre > 0)
            {
                string erreursHTML = "<ul>";
                foreach (string erreur in _listeDesErreurs)
                    erreursHTML = string.Concat(erreursHTML, "<li>", erreur, "</li>");
                erreursHTML = string.Concat(erreursHTML, "</ul>");
                return erreursHTML;
            }
            return string.Empty;
        }
    }
}
