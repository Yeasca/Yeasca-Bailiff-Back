
namespace Yeasca.Metier
{
    public class Secretaire : Profile
    {
        public override string obtenirLaDescription()
        {
            if(nAPasLesInformationsNécessaires())
                return string.Empty;
            return _chaineParAbréviation[Abréviation] + " " + Prénom + " " + Nom;
        }

        protected override bool nAPasLesInformationsNécessaires()
        {
            return string.IsNullOrEmpty(Nom)
                || string.IsNullOrEmpty(Prénom);
        }
    }
}
