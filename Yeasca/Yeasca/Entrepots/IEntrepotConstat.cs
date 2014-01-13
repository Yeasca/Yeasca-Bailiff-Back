using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotConstat : IEntrepot
    {
        Constat récupérerLeConstat(Guid id);
        IList<Constat> récupérerLaListeDesConstats(IRechercheConstat recherche);
        IList<Constat> récupérerLaListeDesConstats(IRechercheGlobale recherche);
        IList<Constat> récupérerLaListeDesConstatsDuClient(Guid idClient);
        int nombreDeConstatPourLeClient(Guid idClient);
    }
}
