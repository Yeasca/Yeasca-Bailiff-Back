using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotConstat : IEntrepot
    {
        Constat récupérerLeConstat(Guid id);
        IList<Constat> récupérerLaListeDesConstats(IRechercheConstat recherche);
    }
}
