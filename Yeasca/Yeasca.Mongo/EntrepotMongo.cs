

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
            return paramètresParDéfaut;
        }

        public static void paramétrer(ModuleInjection paramètres)
        {
            _injecteurEntrepot = new Injecteur(paramètres);
        }

        public EntrepotMongo()
        {
            if (_injecteurEntrepot == null)
            {
                ModuleInjection paramètres = obtenirLesParamètresParDéfaut();
                paramètres.lier<IFournisseur>().à<FournisseurMongo>();
                _injecteurEntrepot = new Injecteur(paramètres);
            }
        }

        public T fabriquerEntrepot<T>() where T : IEntrepot
        {
            return _injecteurEntrepot.construire<T>();
        }
    }
}
