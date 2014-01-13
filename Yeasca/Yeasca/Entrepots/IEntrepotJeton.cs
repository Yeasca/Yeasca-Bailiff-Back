
namespace Yeasca.Metier
{
    public interface IEntrepotJeton : IEntrepot
    {
        bool aEtéUtilisé(string jeton);
    }
}
