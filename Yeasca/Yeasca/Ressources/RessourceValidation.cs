using System.Reflection;
using System.Resources;

namespace Yeasca.Ressources
{
    public class RessourceValidation : Ressource
    {
        private const string _nomDuFichier = "RessourcesOrganisme";

        private new static bool initialiser()
        {
            try
            {
                if (_ressources == null)
                    _ressources = new ResourceManager(_nomDuFichier, Assembly.GetExecutingAssembly());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static readonly int LONGUEUR_MAX_NUMÉRO_VOIE = enNombre("LONGUEUR_MAX_NUMÉRO_VOIE", 5);
        public static readonly int LONGUEUR_MAX_RÉPÉTITION = enNombre("LONGUEUR_MAX_RÉPÉTITION", 9);

        public static readonly string NUMÉRO_VOIE_REQUIS = enChaine("NUMÉRO_VOIE_REQUIS");
        public static readonly string NUMÉRO_VOIE_INVALIDE = enChaine("NUMÉRO_VOIE_INVALIDE");
        public static readonly string NUMÉRO_VOIE_LONGUEUR_MAX = enChaine("NUMÉRO_VOIE_LONGUEUR_MAX");
        public static readonly string RÉPÉTITION_VOIE_LONGUEUR_MAX = enChaine("RÉPÉTITION_VOIE_LONGUEUR_MAX");

        public static readonly int LONGUEUR_MAX_TYPE_VOIE = enNombre("LONGUEUR_MAX_TYPE_VOIE", 255);
        public static readonly int LONGUEUR_MAX_NOM_VOIE = enNombre("LONGUEUR_MAX_NOM_VOIE", 255);
        public static readonly int LONGUEUR_MAX_COMPLÉMENT_VOIE = enNombre("LONGUEUR_MAX_COMPLÉMENT_VOIE", 255);

        public static readonly string TYPE_VOIE_REQUIS = enChaine("TYPE_VOIE_REQUIS"); 
        public static readonly string TYPE_VOIE_LONGUEUR_MAX = enChaine("TYPE_VOIE_LONGUEUR_MAX");
        public static readonly string NOM_VOIE_REQUIS = enChaine("NOM_VOIE_REQUIS");
        public static readonly string NOM_VOIE_LONGUEUR_MAX = enChaine("NOM_VOIE_LONGUEUR_MAX");
        public static readonly string COMPLÉMENT_VOIE_LONGUEUR_MAX = enChaine("COMPLÉMENT_VOIE_LONGUEUR_MAX");

        public static readonly int LONGUEUR_MAX_COMMUNE = enNombre("LONGUEUR_MAX_COMMUNE", 255);

        public static readonly string CODE_POSTAL_REQUIS = enChaine("CODE_POSTAL_REQUIS");
        public static readonly string CODE_POSTAL_INVALIDE = enChaine("CODE_POSTAL_INVALIDE");
        public static readonly string LIBELLÉ_COMMUNE_REQUIS = enChaine("LIBELLÉ_COMMUNE_REQUIS");
        public static readonly string LIBELLÉ_COMMUNE_LONGUEUR_MAX = enChaine("LIBELLÉ_COMMUNE_LONGUEUR_MAX");
    }
}
