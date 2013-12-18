using System;

namespace Yeasca.Metier
{
    public interface ISession
    {
        Guid ID { get; set; }
        DateTime DateConnexion { get; set; }
    }
}
