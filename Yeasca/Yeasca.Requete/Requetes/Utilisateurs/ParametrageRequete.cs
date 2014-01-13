
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class ParametrageRequete : Requete<IParametrageMessage>
    {
        public override ReponseRequete exécuter(IParametrageMessage message)
        {
            if (estAuthentifié())
            {
                IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
                ParametrageReponse résultat = new ParametrageReponse();
                résultat.Répétition = paramètres.récupérerLesRépétitionDeVoie() as List<string>;
                résultat.TypeVoie = paramètres.récupérerLesTypesDeVoie() as List<string>;
                résultat.Civilités = paramètres.récupérerLesCivilités() as Dictionary<int, string>;
                résultat.TypeUtilisateur = paramètres.récupérerLesCivilités() as Dictionary<int, string>;
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }
    }

    public class ParametrageReponse
    {
        public List<string> Répétition { get; set; }
        public List<string> TypeVoie { get; set; }
        public Dictionary<int, string> Civilités { get; set; }
        public Dictionary<int, string> TypeUtilisateur { get; set; }
    }
}
