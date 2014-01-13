
namespace Yeasca.Commande
{
    public interface IModifierSecretaireMessage : IMessageCommande
    {
        string IdSecrétaire { get; set; }
        string Email { get; set; }
        string MotDePasse { get; set; }
        int Civilité { get; set; }
        string Nom { get; set; }
        string Prénom { get; set; }
    }
}
