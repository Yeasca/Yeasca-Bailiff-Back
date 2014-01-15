using Yeasca.Commande;

namespace Yeasca.Web.Api
{
    public class CreerClientMessage : ICreerClientMessage
    {
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string DénominationSociété { get; set; }
        public string NuméroVoie { get; set; }
        public string RépétitionVoie { get; set; }
        public string TypeVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
}