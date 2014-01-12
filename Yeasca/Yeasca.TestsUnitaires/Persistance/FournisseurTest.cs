using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Persistance
{
    public class FournisseurTest : IFournisseur
    {
        private IDictionary<string, IList<IAgregat>> _collections;

        public FournisseurTest()
        {
            _collections = new Dictionary<string, IList<IAgregat>>();    
        }

        public void seConnecter() { }
        public void seDéconnecter() { }

        public bool EstConnecté
        {
            get
            {
                return true;
            }
        }

        public IQueryable<T> obtenirLaCollection<T>() where T : IAgregat
        {
            string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
            if (_collections.ContainsKey(nomDeLaCollection))
                return convertirLesEléments<T>(nomDeLaCollection);
            return new List<T>().AsQueryable();
        }

        private IQueryable<T> convertirLesEléments<T>(string nomDeLaCollection) where T : IAgregat
        {
            IList<T> éléments = new List<T>();
            foreach (IAgregat agrégat in _collections[nomDeLaCollection])
                éléments.Add((T) Convert.ChangeType(agrégat, typeof (T)));
            return éléments.AsQueryable<T>();
        }

        private string trouverLeNomDeLaCollectionCorrespondante<T>()
        {
            Type typeDeLAggregat = typeof(T);
            return typeDeLAggregat.GetTypeInfo().Name;
        }

        public bool insérer<T>(IAgregat agrégat) where T : IAgregat
        {
            try
            {
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                if (_collections.ContainsKey(nomDeLaCollection))
                    return ajouterALaCollection<T>(agrégat, nomDeLaCollection);
                _collections.Add(nomDeLaCollection, new List<IAgregat>());
                agrégat.Id = Guid.NewGuid();
                _collections[nomDeLaCollection].Add(agrégat);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ajouterALaCollection<T>(IAgregat agrégat, string nomDeLaCollection) where T : IAgregat
        {
            IQueryable<T> collectionAModifier = obtenirLaCollection<T>();
            if (!collectionAModifier.Any(x => x.Id == agrégat.Id))
            {
                agrégat.Id = Guid.NewGuid();
                _collections[nomDeLaCollection].Add(agrégat);
                return true;
            }
            return false;
        }

        public bool modifier<T>(IAgregat agrégat) where T : IAgregat
        {
            try
            {
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                if (_collections.ContainsKey(nomDeLaCollection))
                    return modifierLaCollection<T>(agrégat, nomDeLaCollection);
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool modifierLaCollection<T>(IAgregat agrégat, string nomDeLaCollection) where T : IAgregat
        {
            IQueryable<T> collectionAModifier = obtenirLaCollection<T>();
            if (collectionAModifier.Any(x => x.Id == agrégat.Id))
            {
                _collections[nomDeLaCollection] = _collections[nomDeLaCollection].Where(x => (x as IAgregat).Id != agrégat.Id).ToList();
                _collections[nomDeLaCollection].Add(agrégat);
                return true;
            }
            return false;
        }
    }
}
