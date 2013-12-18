using System;
using System.Web;

namespace Yeasca.Metier
{
    public class SessionUtilisateur : ISession
    {
        public Guid ID { get; set; }
        public DateTime DateConnexion { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
