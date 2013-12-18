using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yeasca.Metier
{
    public class ChiffrementAES
    {

        public byte[] crypter(string chaine)
        {
            ICryptoTransform crypteur = obtenirLeChiffrageParamétré().CreateEncryptor();
            using (MemoryStream msCrypteur = new MemoryStream())
            {
                using (CryptoStream csCrypteur = new CryptoStream(msCrypteur, crypteur, CryptoStreamMode.Write))
                {
                    using (StreamWriter swCrypteur = new StreamWriter(csCrypteur))
                    {
                        swCrypteur.Write(chaine);
                    }
                    return msCrypteur.ToArray();
                }
            }
        }

        public string décrypter(byte[] binaire)
        {
            ICryptoTransform décrypteur = obtenirLeChiffrageParamétré().CreateDecryptor();
            using (MemoryStream msDécrypteur = new MemoryStream(binaire))
            {
                using (CryptoStream csDécrypteur = new CryptoStream(msDécrypteur, décrypteur, CryptoStreamMode.Read))
                {
                    using (StreamReader srDécrypteur = new StreamReader(csDécrypteur))
                    {
                        return srDécrypteur.ReadToEnd();
                    }
                }
            }
        }

        private Aes obtenirLeChiffrageParamétré()
        {
            Aes chiffrage = Aes.Create();
            chiffrage.Key = Encoding.Default.GetBytes("Epsil0n3Epsil0n3Epsil0n3Epsil0n3");
            chiffrage.IV = Encoding.Default.GetBytes("y3sCaB@il1Ff.bck");
            return chiffrage;
        }
    }
}
