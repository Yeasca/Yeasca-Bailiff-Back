using System;

namespace Yeasca.Metier
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class InjecterAttribute : Attribute { }
}
