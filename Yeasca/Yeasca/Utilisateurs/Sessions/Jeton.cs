using System;
using System.Security.Cryptography;
using System.Text;

namespace Yeasca.Metier
{
    public class Jeton : IAgregat
    {
        private static readonly SHA256 _hasheur = new SHA256Managed();
        private const string SEL_1 = "niouhndç_";
        private const string SEL_2 = "zaioujeç&_";

        public Jeton()
        {
        }

        public Jeton(string valeur)
        {
            Valeur = valeur;
        }

        public Guid Id { get; set; }
        public string Valeur { get; set; }

        public bool estValide(string clé)
        {
            return générerUnJeton(clé) == Valeur;
        }

        public static string générerUnJeton(string clé)
        {
            string chaineAChiffrer = string.Concat(SEL_1, clé, SEL_2);
            byte[] chaineChiffrée = _hasheur.ComputeHash(Encoding.Default.GetBytes(chaineAChiffrer));
            return Encoding.Default.GetString(chaineChiffrée);
        }
    }
}
