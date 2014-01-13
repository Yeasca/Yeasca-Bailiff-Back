
namespace Yeasca.Commande
{
    public interface ICreerHuissierMessage : IMessageCommande
    {
        string Email { get; set; }
        string MotDePasse { get; set; }
        int Civilité { get; set; }
        string Nom { get; set; }
        string Prénom { get; set; }
    }
}
