namespace Yeasca.Metier
{
    public interface IRessourceParametre
    {
        //Type utilisateur
        string LIBELLÉ_HUISSIER { get; }
        string LIBELLÉ_SECRÉTAIRE { get; }

        //Civilité
        string MADEMOISELLE { get; }
        string MADAME { get; }
        string MONSIEUR { get; }

        //Autre
        string FORMAT_DATE { get; }
    }
}
