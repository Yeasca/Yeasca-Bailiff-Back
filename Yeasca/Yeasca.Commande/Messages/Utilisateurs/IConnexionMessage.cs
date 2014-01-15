
namespace Yeasca.Commande
{
    public interface IConnexionMessage : IMessageCommande
    {
        string Email { get; set; }
        string MotDePasse { get; set; }
    }
}
