namespace Yeasca.Commande
{
    public class ReponseCommande
    {
        public bool Réussite { get; set; }
        public string Message { get; set; }

        public static ReponseCommande générerUnSuccès(string message)
        {
            return new ReponseCommande() { Réussite = true, Message = message};
        }

        public static ReponseCommande générerUnSuccès()
        {
            return générerUnSuccès(null);
        }

        public static ReponseCommande générerUnEchec(string message)
        {
            return new ReponseCommande() { Réussite = false, Message = message};
        }
    }
}
