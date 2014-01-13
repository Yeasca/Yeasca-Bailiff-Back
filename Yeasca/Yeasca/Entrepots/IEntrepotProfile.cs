using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotProfile : IEntrepot
    {
        Profile récupérerLeClient(Guid id);
        Huissier récupérerLHuissier(Guid id);
        Secretaire récupérerLaSecrétaire(Guid id);
        IList<Client> récupérerLaListeDesClients(IRechercheGlobale recherche);
        IList<Huissier> récupérerLaListeDesHuissier(IRechercheGlobale recherche);
        IList<Secretaire> récupérerLaListeDesSecrétaires(IRechercheGlobale recherche);
    }
}
