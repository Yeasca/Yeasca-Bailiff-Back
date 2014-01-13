using System;

namespace Yeasca.Metier
{
    public interface IRechercheUtilisateur : IRecherche
    {
        string NomUtilisateur { get; set; }
        TypeUtilisateur Type { get; set; }
    }
}
