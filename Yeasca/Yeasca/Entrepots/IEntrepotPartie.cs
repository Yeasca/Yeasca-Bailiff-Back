using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotPartie : IEntrepot
    {
        Partie récupérerLeClient(Guid id);
        Huissier récupérerLHuissier(Guid id);
        IList<Huissier> récupérerLaListeDesHuissier(IRechercheGlobale recherche);
        IList<Partie> récupérerLaListeDesClients(IRechercheGlobale recherche);
    }
}
