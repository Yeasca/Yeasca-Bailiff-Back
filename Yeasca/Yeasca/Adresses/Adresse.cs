
namespace Yeasca.Metier
{
    public class Adresse
    {
        public Adresse()
        {
            initialiser();
        }

        public Adresse(
            string numéroVoie,
            string répétitionVoie,
            string typeVoie,
            string nomVoie,
            string complémentVoie,
            string codePostal,
            string libelléCommune)
        {
            initialiser();
            initialiserLaVoie(numéroVoie, répétitionVoie, typeVoie, nomVoie, complémentVoie);
            initialiserLaVille(codePostal, libelléCommune);
        }

        private void initialiser()
        {
            Voie = new Voie();
            Ville = new Commune();
        }

        private void initialiserLaVoie(
            string numéroVoie, 
            string répétitionVoie, 
            string typeVoie, 
            string nomVoie, 
            string complémentVoie)
        {
            Voie.NuméroVoie.Numéro = numéroVoie;
            Voie.NuméroVoie.Répétition = répétitionVoie;
            Voie.TypeVoie = typeVoie;
            Voie.NomVoie = nomVoie;
            Voie.ComplémentVoie = complémentVoie;
        }

        private void initialiserLaVille(string codePostal, string libelléCommune)
        {
            Ville.CodePostal.Code = codePostal;
            Ville.LibelléCommune = libelléCommune;
        }

        public Voie Voie { get; set; }
        public Commune Ville { get; set; }

        public  bool estValide()
        {
            return Voie.estValide() 
                && Ville.estValide();
        }

        public Erreur obtenirLesErreurs()
        {
            Erreur message = new Erreur();
            validerLaVoie(message);
            validerLaVille(message);
            return message;
        }

        private void validerLaVoie(Erreur message)
        {
            if (!Voie.estValide())
                message.ajouterUneErreur(Voie.obtenirLesErreurs());
        }

        private void validerLaVille(Erreur message)
        {
            if (!Ville.estValide())
                message.ajouterUneErreur(Ville.obtenirLesErreurs());
        }

        public string enChaine()
        {
            return string.Concat(Voie.enChaine(), " ", Ville.enChaine()).Trim();
        }

        public string enChaineAvecUnSéparateur(string séparateur)
        {
            return string.Concat(Voie.enChaine(), " ", séparateur, " ", Ville.enChaine());
        }

        public string enChaineAvecUnRetourALaLigne(string retour)
        {
            return string.Concat(Voie.enChaine(), retour, Ville.enChaine());
        }
    }
}
