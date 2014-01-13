using System.IO;

namespace Yeasca.Commande
{
    public interface IAjouterFichierConstatMessage : IMessageCommande
    {
        string IdConstat { get; set; }
        MemoryStream Fichier { get; set; }
        string Nom { get; set; }
        string Extension { get; set; }
    }
}
