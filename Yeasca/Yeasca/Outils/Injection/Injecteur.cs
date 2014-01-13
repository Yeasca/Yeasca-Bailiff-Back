using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yeasca.Metier
{
    public class Injecteur
    {
        private ModuleInjection _dépendances;
        private static Dictionary<Type, object> _magasin = new Dictionary<Type, object>(); 

        public Injecteur(ModuleInjection module)
        {
            _magasin = new Dictionary<Type, object>();
            _dépendances = module;
        }

        public T construire<T>()
        {
            return (T)construire(typeof(T));
        }

        public object construire(Type unType)
        {
            Injection injectionRésolue = résoudreLInjection(unType);
            if (injectionRésolue.TypeInjection == TypeInjection.Singleton)
                return prendreLInstanceDuMagasin(injectionRésolue);
            return créerUneInstance(injectionRésolue);
        }

        private object prendreLInstanceDuMagasin(Injection injectionRésolue)
        {
            if (!_magasin.ContainsKey(injectionRésolue.Type))
                _magasin.Add(injectionRésolue.Type, créerUneInstance(injectionRésolue));
            return _magasin[injectionRésolue.Type];
        }

        private object créerUneInstance(Injection injectionRésolue)
        {
            ConstructorInfo constructeurÀUtiliser = trouverLeConstructeur(injectionRésolue.Type);
            ParameterInfo[] paramètres = constructeurÀUtiliser.GetParameters();
            if (!paramètres.Any())
                return Activator.CreateInstance(injectionRésolue.Type);
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

        private Injection résoudreLInjection(Type unType)
        {
            Injection injectionRésolue = _dépendances.résoudre(unType);
            if (injectionRésolue == null)
                throw new InjectionException();
            return injectionRésolue;
        }
    }
}
