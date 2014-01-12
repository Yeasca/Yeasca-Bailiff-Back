using System;

namespace Yeasca.Metier
{
    public interface IRechercheConstat : IRecherche
    {
        string nomClient { get; set; }
        DateTime? dateDébut { get; set; }
        DateTime? dateFin { get; set; }
    }
}
