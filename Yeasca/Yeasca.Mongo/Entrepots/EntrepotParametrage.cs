using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotParametrage : IEntrepotParametrage
    {
        private IFournisseur _fournisseur;

        public EntrepotParametrage(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }

        public bool ajouter(IAgregat agrégat)
        {
            throw new NotImplementedException();
        }

        public bool modifier(IAgregat agrégat)
        {
            throw new NotImplementedException();
        }

        public IList<string> récupérerLesRépétitionDeVoie()
        {
            return
                _fournisseur.obtenirLaCollection<ParametreMongo>()
                    .Where(x => x is ParametreRepetition)
                    .OrderBy(x => x.Valeur)
                    .Select(x => x.Valeur)
                    .ToList();
        }

        public IList<string> récupérerLesTypesDeVoie()
        {
            return
                _fournisseur.obtenirLaCollection<ParametreMongo>()
                    .Where(x => x is ParametreTypeVoie)
                    .OrderBy(x => x.Valeur)
                    .Select(x => x.Valeur)
                    .ToList();
        }

        public IDictionary<TypeUtilisateur, string> récupérerLesTypesUtilisateurs()
        {
            return new Dictionary<TypeUtilisateur, string>()
            {
                {TypeUtilisateur.Huissier, Ressource.Paramètres.LIBELLÉ_HUISSIER},
                {TypeUtilisateur.Secrétaire, Ressource.Paramètres.LIBELLÉ_SECRÉTAIRE}
            };
        }

        public IDictionary<Abreviation, string> récupérerLesCivilités()
        {
            return new Dictionary<Abreviation, string>()
            {
                {Abreviation.Mademoiselle, Ressource.Paramètres.MADEMOISELLE},
                {Abreviation.Madame, Ressource.Paramètres.MADAME},
                {Abreviation.Monsieur, Ressource.Paramètres.MONSIEUR}
            };
        }
    }

    public class ParametreMongo : IAgregat
    {
        public Guid Id { get; set; }
        public string Valeur { get; set; }
    }

    public class ParametreRepetition : ParametreMongo { }
    public class ParametreTypeVoie : ParametreMongo { }
}
