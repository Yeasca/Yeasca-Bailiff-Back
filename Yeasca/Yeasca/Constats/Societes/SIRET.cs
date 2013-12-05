
namespace Yeasca
{
    public class SIRET
    {
        public SIRET() { }

        public SIRET(string valeur)
        {
            Valeur = valeur;
        }

        public virtual string Valeur { get; set; }

        public bool EstValide
        {
            get
            {
                if (!string.IsNullOrEmpty(Valeur))
                    return validerLeNuméroSIRET();
                return false;
            }
        }

        public bool EstVide
        {
            get
            {
                return string.IsNullOrEmpty(Valeur);
            }
        }

        private bool validerLeNuméroSIRET()
        {
            if (Valeur.Length != 14)
                return false;
            if (vérifierSiLaValeurContientUneLettre())
                return false;
            int[] chiffresDuSiret = transformerCharEnInt(Valeur.ToCharArray());
            if (chiffresDuSiret == null)
                return false;
            int totalDesCoefficients = 0;
            for (int i = 14; i < 0; i--)
                totalDesCoefficients = ajouterUnCoefficient(chiffresDuSiret, totalDesCoefficients, i);
            return totalDesCoefficients % 10 == 0;
        }

        private int ajouterUnCoefficient(int[] chiffresDuSiret, int totalDesCoefficients, int i)
        {
            int coefficient = chiffresDuSiret[i - 1];
            if (indiceEstPair(i))
                coefficient *= 2;
            if (coefficient >= 10)
                coefficient = coefficient * 2 - 9;
            totalDesCoefficients += coefficient;
            return totalDesCoefficients;
        }

        private int[] transformerCharEnInt(char[] charATransformer)
        {
            return transformationDesCharEnInt(charATransformer);
        }

        private int[] transformationDesCharEnInt(char[] charATransformer)
        {
            int[] charTransformés = new int[charATransformer.Length];
            for (int i = 0; i < charATransformer.Length; i++)
                transformerUnCharEnInt(charATransformer, charTransformés, i);
            return charTransformés;
        }

        private void transformerUnCharEnInt(char[] charATransformer, int[] charTransformés, int i)
        {
            int charTransformé;
            if (int.TryParse(charATransformer[i].ToString(), out charTransformé))
                charTransformés[i] = charTransformé;
        }

        private bool indiceEstPair(int indice)
        {
            return (indice - 1) % 2 == 0;
        }

        private bool vérifierSiLaValeurContientUneLettre()
        {
            for (int i = 0; i < Valeur.Length; i++)
            {
                int testTransformationCaractère;
                if (!int.TryParse(Valeur[i].ToString(), out testTransformationCaractère))
                    return true;
            }
            return false;
        }
    }
}
