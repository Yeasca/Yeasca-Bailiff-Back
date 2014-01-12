using System.Linq;

namespace Yeasca.Metier
{
    public interface IFournisseur
    {
        void seConnecter();
        void seDéconnecter();
        bool EstConnecté { get; }
        IQueryable<T> obtenirLaCollection<T>() where T : IAgregat;
        bool insérer<T>(IAgregat agrégat) where T : IAgregat;
        bool modifier<T>(IAgregat agrégat) where T: IAgregat;
    }
}
