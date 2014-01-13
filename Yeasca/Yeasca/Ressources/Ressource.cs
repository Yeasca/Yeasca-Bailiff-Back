
namespace Yeasca.Metier
{
    public class Ressource
    {
        public static IRessourceValidation Validation = new RessourceValidationFrance();
        public static IRessourceParametre Paramètres = new RessourceParametreFrance();
        public static IRessourceCommande Commandes = new RessourceCommandeFrance();
        public static IRessourceRequete Requetes = new RessourceRequeteFrance();
    }
}
