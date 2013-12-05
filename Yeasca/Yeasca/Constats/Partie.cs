using System;

namespace Yeasca
{
    public abstract class Partie
    {
        public Partie()
        {
            Société = new Societe();
        }

        public Guid ID { get; set; }
        public Societe Société { get; set; }

        public abstract string obtenirLaDescription();
    }
}
