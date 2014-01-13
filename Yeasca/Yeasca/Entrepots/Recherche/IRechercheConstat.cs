using System;

namespace Yeasca.Metier
{
    public interface IRechercheConstat : IRecherche
    {
        string NomClient { get; set; }
        DateTime? DateDébut { get; set; }
        DateTime? DateFin { get; set; }
    }
}
