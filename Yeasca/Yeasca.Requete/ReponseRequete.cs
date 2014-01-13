namespace Yeasca.Requete
{
    public class ReponseRequete
    {
        public bool Réussite { get; set; }
        public string Message { get; set; }
        public object Résultat { get; set; }

        public static ReponseRequete générerUnSuccès(object résultat, string message)
        {
            return new ReponseRequete(){ Réussite =  true, Message = message, Résultat = résultat}; 
        }

        public static ReponseRequete générerUnSuccès(object résultat)
        {
            return générerUnSuccès(résultat, null);
        }

        public static ReponseRequete générerUnEchec(string message)
        {
            return new ReponseRequete(){ Réussite = false, Message = message};
        }
    }
}
