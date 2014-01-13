using System.Collections.Generic;

namespace Yeasca.Metier
{
    public interface IEntrepotParametrage : IEntrepot
    {
        IList<string> récupérerLesRépétitionDeVoie();
        IList<string> récupérerLesTypesDeVoie();
        IDictionary<TypeUtilisateur, string> récupérerLesTypesUtilisateurs();
        IDictionary<Abreviation, string> récupérerLesCivilités();
    }
}
