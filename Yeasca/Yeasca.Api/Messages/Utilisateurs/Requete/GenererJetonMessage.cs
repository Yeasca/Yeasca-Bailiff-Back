using Yeasca.Requete;

namespace Yeasca.Api
{
    public class GenererJetonMessage : IGenererJetonMessage
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string MotDePasse { get; set; }
    }
}