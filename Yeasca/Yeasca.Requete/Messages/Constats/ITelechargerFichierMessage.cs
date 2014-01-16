
namespace Yeasca.Requete
{
    public interface ITelechargerFichierMessage : IMessageRequete
    {
        string IdConstat { get; set; }
        string IdFichier { get; set; }
    }
}
