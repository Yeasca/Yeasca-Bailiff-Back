using System;
using System.Collections.Generic;
using System.Linq;

namespace Yeasca.Metier
{
    public class ModuleInjection
    {
        private Dictionary<Type, Type> _dépendances = new Dictionary<Type, Type>();

        public int NombreDeDépendances
        {
            get
            {
                return _dépendances.Count;
            }
        }

        public Type résoudre<T>()
        {
            if (aLiéLeType<T>())
                return _dépendances[typeof(T)];
            return null;
        }

        public Type résoudre(Type unType)
        {
            if (aLiéLeType(unType))
                return _dépendances[unType];
            return null;
        }

        public Injection lier<T>()
        {
            Injection liaison = new Injection(this, typeof(T));
            return liaison;
        }

        public bool aLiéLeType<T>()
        {
            return _dépendances.Keys.Any(clé => clé.Equals(typeof(T)));
        }

        public bool aLiéLeType(Type unType)
        {
            return _dépendances.Keys.Any(clé => clé.Equals(unType));
        }

        public class Injection
        {
            private ModuleInjection _module;
            private Type _typeÀLier;
            internal Injection(ModuleInjection module, Type typeÀLier)
            {
                _module = module;
                _typeÀLier = typeÀLier;
            }


            public void à<T>()
            {
                validerLaLiaison<T>();
                if (_module.aLiéLeType<T>())
                    _module._dépendances[_typeÀLier] = typeof(T);
                _module._dépendances.Add(_typeÀLier, typeof(T));
            }

            private void validerLaLiaison<T>()
            {
                if (typeof(T).IsInterface)
                    throw new InjectionException();
                if (!typeof(T).GetInterfaces().Any(x => x.Equals(_typeÀLier)))
                    throw new InjectionException();
            }

            public void àLuiMême()
            {
                validerLaRéflexion();
                _module._dépendances.Add(_typeÀLier, _typeÀLier);
            }

            private void validerLaRéflexion()
            {
                if (_typeÀLier.IsInterface)
                    throw new InjectionException();
            }
        }
    }
}
