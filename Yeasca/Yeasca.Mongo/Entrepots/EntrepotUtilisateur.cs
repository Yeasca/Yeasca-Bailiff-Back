
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotUtilisateur : Entrepot, IEntrepotUtilisateur
    {
        private IFournisseur _fournisseur;

        public EntrepotUtilisateur(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }

        public bool ajouter(IAgregat agrégat)
        {
            if(agrégat is Utilisateur)
                return _fournisseur.insérer<Utilisateur>(agrégat);
            return false;
        }

        public bool modifier(IAgregat agrégat)
        {
            if(agrégat is Utilisateur)
                return _fournisseur.modifier<Utilisateur>(agrégat);
            return false;
        }

        public Utilisateur authentifier(Email email, MotDePasse motDePasse)
        {
            if (email != null && motDePasse != null)
            {
                Utilisateur utilisateurAuthentifié = _fournisseur.obtenirLaCollection<Utilisateur>()
                    .FirstOrDefault(x =>
                        x.Email.Valeur != null
                        && x.MotDePasse.Valeur != null
                        && x.Email.Equals(email)
                        && x.MotDePasse.Equals(motDePasse));
                return utilisateurAuthentifié;
            }
            return null;
        }

        public Utilisateur récupérer(Guid Id)
        {
            return _fournisseur.obtenirLaCollection<Utilisateur>().SingleOrDefault(x => x.Id == Id);
        }

        public IList<Utilisateur> récupérerLaListeDesUtilisateurs(IRechercheUtilisateur recherche)
        {
            IQueryable<Utilisateur> résultats =
                _fournisseur.obtenirLaCollection<Utilisateur>().OrderBy(x => x.Profile.Nom);
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Utilisateur> filtrerLaRecherche(IRechercheUtilisateur recherche, IQueryable<Utilisateur> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.NomUtilisateur))
            {
                résultats = résultats.Where(x =>
                    x.Profile.Nom != null && x.Profile.Nom.IndexOf(recherche.NomUtilisateur, StringComparison.OrdinalIgnoreCase) > -1
                    || x.Profile.Prénom != null && x.Profile.Prénom.IndexOf(recherche.NomUtilisateur, StringComparison.OrdinalIgnoreCase) > -1);
            }
            if (faitUneRechercheSurLeTypeDUtilisateur(recherche))
            {
                résultats = résultats.Where(x => x.TypeUtilisateur == (TypeUtilisateur)recherche.Type);
            }
            return résultats;
        }

        private bool faitUneRechercheSurLeTypeDUtilisateur(IRechercheUtilisateur recherche)
        {
            return (TypeUtilisateur)(recherche.Type) != TypeUtilisateur.Inconnu;
        }
    }
}
