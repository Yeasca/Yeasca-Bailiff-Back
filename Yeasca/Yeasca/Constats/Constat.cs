using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Constat
    {
        public Constat()
        {
            Fichiers = new List<string>();    
        }

        public Guid ID { get; set; }
        public Partie Client { get; set; }
        public Partie Huissier { get; set; }
        public IList<string> Fichiers { get; set; }

        public string générerLeModèleDuConstat()
        {
            throw new NotImplementedException();
        }
    }
}
