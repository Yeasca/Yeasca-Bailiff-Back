
namespace Yeasca.Commande
{
    public interface ICreerSecretaireMessage : IMessageCommande
    {
        string Email { get; set; }
        string MotDePasse { get; set; }
        int Civilité { get; set; }
        string Nom { get; set; }
        string Prénom { get; set; }
    }
}
