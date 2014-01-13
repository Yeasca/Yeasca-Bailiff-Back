namespace Yeasca.Commande
{
    public interface ICreerConstatMessage : IMessageCommande
    {
        string IdClient { get; set; }
    }
}
