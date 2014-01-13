using System;

namespace Yeasca.Commande
{
    public interface ICommande<T> where T : IMessageCommande
    {
        ReponseCommande exécuter(T message);
    }

    public interface ICommande
    {
        Type Message { get; }
    }
}
