using System;

namespace Yeasca.Metier
{
    public interface IRechercheUtilisateur : IRecherche
    {
        Guid Id { get; set; }
        string Nom { get; set; }
        string Prénom { get; set; }
        TypeUtilisateur Type { get; set; }
    }
}
