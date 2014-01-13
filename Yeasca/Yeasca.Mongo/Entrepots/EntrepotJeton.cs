using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotJeton : IEntrepotJeton
    {
        private IFournisseur _fournisseur;

        public EntrepotJeton(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }

        public bool ajouter(IAgregat agrégat)
        {
            return _fournisseur.insérer<Jeton>(agrégat);
        }

        public bool modifier(IAgregat agrégat)
        {
            throw new System.NotImplementedException();
        }

        public bool aEtéUtilisé(string valeurDuJeton)
        {
            return _fournisseur.obtenirLaCollection<Jeton>().Any(x => x.Valeur == valeurDuJeton);
        }
    }
}
