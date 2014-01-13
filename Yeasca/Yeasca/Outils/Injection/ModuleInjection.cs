using System;
using System.Collections.Generic;
using System.Linq;

namespace Yeasca.Metier
{
    public class ModuleInjection
    {
        private Dictionary<Type, Injection> _dépendances = new Dictionary<Type, Injection>();

        public int NombreDeDépendances
        {
            get
            {
                return _dépendances.Count;
            }
        }

        public Injection résoudre<T>()
        {
            if (aLiéLeType<T>())
                return _dépendances[typeof(T)];
            return null;
        }

        public Injection résoudre(Type unType)
        {
            if (aLiéLeType(unType))
                return _dépendances[unType];
            return null;
        }

        public LiaisonInjection lier<T>()
        {
            LiaisonInjection liaison = new LiaisonInjection(this, typeof(T));
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

        public class LiaisonInjection
        {
            private ModuleInjection _module;
            private Type _typeÀLier;
            internal LiaisonInjection(ModuleInjection module, Type typeÀLier)
            {
                _module = module;
                _typeÀLier = typeÀLier;
            }


            public ParametreLiaisonInjection à<T>()
            {
                validerLaLiaison<T>();
                if (_module.aLiéLeType<T>())
                    _module._dépendances[_typeÀLier] = new Injection() {Type = typeof (T)};
                _module._dépendances.Add(_typeÀLier, new Injection() { Type = typeof(T) });
                return new ParametreLiaisonInjection(_module._dépendances[_typeÀLier]); 
            }

            private void validerLaLiaison<T>()
            {
                if (typeof(T).IsInterface)
                    throw new InjectionException();
                if (!typeof(T).GetInterfaces().Any(x => x.Equals(_typeÀLier)))
                    throw new InjectionException();
            }

            public ParametreLiaisonInjection àLuiMême()
            {
                validerLaRéflexion();
                _module._dépendances.Add(_typeÀLier, new Injection() { Type = _typeÀLier });
                return new ParametreLiaisonInjection(_module._dépendances[_typeÀLier]); 
            }

            private void validerLaRéflexion()
            {
                if (_typeÀLier.IsInterface)
                    throw new InjectionException();
            }

            public class ParametreLiaisonInjection
            {
                private Injection _injection;

                public ParametreLiaisonInjection(Injection injection)
                {
                    _injection = injection;
                }

                public void enTransient()
                {
                    _injection.TypeInjection = TypeInjection.Transient;
                }

                public void enSingleton()
                {
                    _injection.TypeInjection = TypeInjection.Singleton;
                }
            }
        }
    }

    public class Injection
    {
        public Injection()
        {
            TypeInjection = TypeInjection.Transient;
        }

        public Type Type { get; set; }
        public TypeInjection TypeInjection { get; set; }
    }

    public enum TypeInjection
    {
        Transient,
        Singleton
    }
}
