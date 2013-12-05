using System;

namespace Yeasca
{
    public class Societe
    {
        public Societe()
        {
            Adresse = new Adresse();
        }

        public Guid ID { get; set; }
        public string Dénomination { get; set; }
        public SIRET NuméroSIRET { get; set; }
        public Adresse Adresse { get; set; }
    }
}
