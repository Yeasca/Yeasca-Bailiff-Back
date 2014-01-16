using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotUtilisateur : IEntrepot
    {
        Utilisateur authentifier(Email email, MotDePasse motDePasse);
        Utilisateur récupérer(Guid Id);
        IList<Utilisateur> récupérerLaListeDesUtilisateurs(IRechercheUtilisateur recherche);
        Utilisateur récupérerLAdministrateur();
    }
}
