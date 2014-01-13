
namespace Yeasca.Requete
{
    public interface IAuthentificationMessage : IMessageRequete
    {
        string Email { get; set; }
        string MotDePasse { get; set; }
    }
}
