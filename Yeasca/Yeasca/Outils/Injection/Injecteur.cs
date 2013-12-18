using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yeasca.Metier
{
    public class Injecteur
    {
        private ModuleInjection _dépendances;

        public Injecteur(ModuleInjection module)
        {
            _dépendances = module;
        }

        public T construire<T>()
        {
            return (T)construire(typeof(T));
        }

        public object construire(Type unType)
        {
            Type typeRésolu = résoudreLeType(unType);
            ConstructorInfo constructeurÀUtiliser = trouverLeConstructeur(typeRésolu);
            ParameterInfo[] paramètres = constructeurÀUtiliser.GetParameters();
            if (!paramètres.Any())
                return Activator.CreateInstance(typeRésolu);
            return constructeurÀUtiliser.Invoke(paramètres.Select(paramètre => construire(paramètre.ParameterType)).ToArray());
        }

        private ConstructorInfo trouverLeConstructeur(Type typeRésolu)
        {
            ConstructorInfo constructeurÀUtiliser = typeRésolu.GetConstructors().FirstOrDefault(constructeur => aUnConstructeurAvecLAttributInjecter(constructeur.CustomAttributes));
            if (constructeurÀUtiliser == null)
                constructeurÀUtiliser = typeRésolu.GetConstructors().First();
            return constructeurÀUtiliser;
        }

        private bool aUnConstructeurAvecLAttributInjecter(IEnumerable<CustomAttributeData> attributs)
        {
            return attributs.Any(attribut => attribut.AttributeType.Equals(typeof(InjecterAttribute)));
        }

        private Type résoudreLeType(Type unType)
        {
            Type typeRésolu = _dépendances.résoudre(unType);
            if (typeRésolu == null)
                throw new InjectionException();
            return typeRésolu;
        }
    }
}
