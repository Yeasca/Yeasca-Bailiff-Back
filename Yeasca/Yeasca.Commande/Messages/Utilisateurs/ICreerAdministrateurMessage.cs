
namespace Yeasca.Commande
{
    public interface ICreerAdministrateurMessage : IMessageCommande
    {
        string Jeton { get; set; }
        string Email { get; set; }
        string MotDePasse { get; set; }
        int Civilité { get; set; }
        string Nom { get; set; }
        string Prénom { get; set; }
        string NomCabinet { get; set; }
        string NuméroVoie { get; set; }
        string RépétitionVoie { get; set; }
        string TypeVoie { get; set; }
        string NomVoie { get; set; }
        string ComplémentVoie { get; set; }
        string CodePostal { get; set; }
        string Ville { get; set; }
    }
}
