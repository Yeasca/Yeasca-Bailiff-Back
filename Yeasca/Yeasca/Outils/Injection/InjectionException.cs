using System;

namespace Yeasca.Metier
{
    public class InjectionException : Exception
    {
        public InjectionException()
        {
        }

        public override string Message
        {
            get
            {
                return "Erreur d'injection";
            }
        }
    }
}
