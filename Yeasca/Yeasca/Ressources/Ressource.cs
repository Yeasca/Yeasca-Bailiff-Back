using System.Resources;

namespace Yeasca.Ressources
{
    public class Ressource
    {
        protected static ResourceManager _ressources;

        protected static int enNombre(string codeRessource, int nombreParDéfaut)
        {
            if (initialiser())
            {
                int résultat;
                if (int.TryParse(_ressources.GetString(codeRessource), out résultat))
                    return résultat;
            }
            return nombreParDéfaut;
        }

        protected static string enChaine(string codeRessource)
        {
            if (initialiser())
                return _ressources.GetString(codeRessource);
            return codeRessource;
        }

        protected static bool initialiser()
        {
            return false;
        }
    }
}
