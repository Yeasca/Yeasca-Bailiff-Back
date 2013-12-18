﻿using Yeasca.Metier;

namespace Yeasca.TestsUnitaires
{
    public class ConstantesTest
    {
        public static readonly string CHAINE_DE_6 = "111111";
        public static readonly string CHAINE_DE_10 = "aaaaaaaaaa";
        public static readonly string CHAINE_DE_256 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public static readonly SIRET SIRET_VALIDE = new SIRET("12345678900015");
        public static readonly Adresse ADRESSE_VALIDE = new Adresse("10", "Bis", "rue", "Bob l'éponge", "dit le marrant", "33520", "Bruges");
        public static readonly Societe SOCIÉTÉ_VALIDE = new Societe() { Dénomination = "poulou", NuméroSIRET = SIRET_VALIDE, Adresse = ADRESSE_VALIDE};
        public static readonly MotDePasse MOT_DE_PASSE_VALIDE = new MotDePasse("d@RkPouL0u!,");
        public static readonly Email EMAIL_VALIDE = new Email("pouet@poulou.com");
        public static readonly Utilisateur UTILISATEUR_VALIDE = new Utilisateur() {Login = "bob", MotDePasse = MOT_DE_PASSE_VALIDE, Email = EMAIL_VALIDE};
    }
}
