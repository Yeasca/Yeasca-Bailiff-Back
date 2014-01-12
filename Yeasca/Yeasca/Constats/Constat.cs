using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Constat : IAgregat
    {
        public Constat()
        {
            Date = DateTime.Now;
            Fichiers = new List<string>();    
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Partie Client { get; set; }
        public Partie Huissier { get; set; }
        public IList<string> Fichiers { get; set; }

        public string générerLeModèleDuConstat()
        {
            throw new NotImplementedException();
        }
    }
}
