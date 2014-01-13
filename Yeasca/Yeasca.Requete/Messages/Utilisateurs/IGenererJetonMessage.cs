
namespace Yeasca.Requete
{
    public interface IGenererJetonMessage : IMessageRequete
    {
        string Email { get; set; }
        string Login { get; set; }
        string MotDePasse { get; set; }
    }
}
