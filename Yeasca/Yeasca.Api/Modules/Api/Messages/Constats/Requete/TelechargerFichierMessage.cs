using Yeasca.Requete;

namespace Yeasca.Api
{
    public class TelechargerFichierMessage : ITelechargerFichierMessage
    {
        public string IdConstat { get; set; }
        public string IdFichier { get; set; }
    }
}