using System;

namespace Yeasca.Metier
{
    public interface IRechercheUtilisateur : IRecherche
    {
        string NomUtilisateur { get; set; }
        int Type { get; set; }
    }
}
