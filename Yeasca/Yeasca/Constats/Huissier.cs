
namespace Yeasca
{
    public class Huissier : Partie
    {
        public Huissier()
            : base()
        {

        }

        public string Nom { get; set; }
        public string Prénom { get; set; }

        public override string obtenirLaDescription()
        {
            return string.Empty;
        }
    }
}
