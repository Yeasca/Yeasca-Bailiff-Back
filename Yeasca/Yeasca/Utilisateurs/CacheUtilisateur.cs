using System.Runtime.Caching;

namespace Yeasca.Metier
{
    public static class CacheUtilisateur
    {
        private const string _cléSessions = Sessions<SessionUtilisateur>.INDEX_SESSION_CACHE;
        public static void initialiserLeCacheUtilisateur()
        {
            MemoryCache.Default[_cléSessions] = new Sessions<SessionUtilisateur>();  
        }

        public static readonly Sessions<SessionUtilisateur> Sessions = MemoryCache.Default[_cléSessions] as Sessions<SessionUtilisateur>; 
    }
}
