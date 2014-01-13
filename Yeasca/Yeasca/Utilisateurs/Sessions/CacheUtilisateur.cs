using System;
using System.Runtime.Caching;

namespace Yeasca.Metier
{
    public static class CacheUtilisateur
    {
        private const string _cléSessions = Sessions<SessionUtilisateur>.INDEX_SESSION_CACHE;

        public static void initialiserLeCacheUtilisateur()
        {
            if (!MemoryCache.Default.Contains(_cléSessions))
                réinitialiserLeCacheUtilisateur();
        }

        public static void réinitialiserLeCacheUtilisateur()
        {
            if (MemoryCache.Default.Contains(_cléSessions))
                MemoryCache.Default.Remove(_cléSessions);
            CacheItemPolicy paramètres = new CacheItemPolicy();
            MemoryCache.Default.Set(_cléSessions, new Sessions<SessionUtilisateur>(), paramètres);
        }

        public static Sessions<SessionUtilisateur> Sessions
        {
            get
            {
                return MemoryCache.Default[_cléSessions] as Sessions<SessionUtilisateur>;
            }
        }
    }
}
