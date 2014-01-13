using System;

namespace Yeasca.Requete
{
    public interface IRequete<T> where T : IMessageRequete
    {
        ReponseRequete exécuter(T message);
    }

    public interface IRequete
    {
        Type Message { get; }
    }
}
