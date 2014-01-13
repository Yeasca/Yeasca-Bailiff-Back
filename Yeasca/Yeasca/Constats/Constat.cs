using System;
using System.Collections.Generic;
using System.Linq;

namespace Yeasca.Metier
{
    public class Constat : IAgregat
    {
        public Constat()
        {
            Date = DateTime.Now;
            Fichiers = new List<Fichier>();    
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Profile Client { get; set; }
        public Profile Huissier { get; set; }
        public IList<Fichier> Fichiers { get; set; }

        public bool EstValidé
        {
            get
            {
                return Fichiers.Any(x => x.EstUnDocumentWord);
            }
        }

        public string générerLeModèleDuConstat()
        {
            throw new NotImplementedException();
        }
    }
}
