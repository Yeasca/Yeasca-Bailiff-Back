using System.IO;
using Yeasca.Commande;

namespace Yeasca.Api
{
    public class AjouterFichierConstatMessage : IAjouterFichierConstatMessage
    {
        public string IdConstat { get; set; }
        public MemoryStream Fichier { get; set; }
        public string Nom { get; set; }
        public string Extension { get; set; }
    }
}