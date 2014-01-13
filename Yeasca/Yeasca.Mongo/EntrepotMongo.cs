using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotMongo
    {
        private static Injecteur _injecteurEntrepot;

        public static ModuleInjection obtenirLesParamètresParDéfaut()
        {
            ModuleInjection paramètresParDéfaut = new ModuleInjection();
            paramètresParDéfaut.lier<IEntrepotConstat>().à<EntrepotConstat>();
            paramètresParDéfaut.lier<IEntrepotProfile>().à<EntrepotProfile>();
            paramètresParDéfaut.lier<IEntrepotUtilisateur>().à<EntrepotUtilisateur>();
            paramètresParDéfaut.lier<IEntrepotParametrage>().à<EntrepotParametrage>();
            paramètresParDéfaut.lier<IEntrepotJeton>().à<EntrepotJeton>();
            return paramètresParDéfaut;
        }

        public static void paramétrer(ModuleInjection paramètres)
        {
            _injecteurEntrepot = new Injecteur(paramètres);
        }

        private static void initialiser()
        {
            if (_injecteurEntrepot == null)
            {
                ModuleInjection paramètres = obtenirLesParamètresParDéfaut();
                paramètres.lier<IFournisseur>().à<FournisseurMongo>();
                _injecteurEntrepot = new Injecteur(paramètres);
            }
        }

        private EntrepotMongo() { }

        public static T fabriquerEntrepot<T>() where T : IEntrepot
        {
            initialiser();
            return _injecteurEntrepot.construire<T>();
        }
    }
}
