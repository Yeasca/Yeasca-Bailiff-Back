using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yeasca.Metier
{
    public class Sessions<T> where T : ISession
    {
        public const string INDEX_SESSION_CACHE = "session";
        public const string INDEX_SESSION_COOKIE = "pouet";
        public const int DÉLAI_SESSION = 8;

        private Dictionary<string, T> _sessions;

        public Sessions()
        {
            _sessions = new Dictionary<string, T>();
        }

        public string ajouterUneSession(T session)
        {
            Guid nouvelID = new Guid();
            session.ID = nouvelID;
            session.DateConnexion = DateTime.Now;
            string nouvelIDEnChaine = nouvelID.ToString();
            _sessions.Add(nouvelIDEnChaine, session);
            modifierLeCookieAvecLIDSession(nouvelIDEnChaine);
            return nouvelIDEnChaine;
        }

        private void modifierLeCookieAvecLIDSession(string idSession)
        {
            HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
            if (unDesCookiesContientUneSession(cookies))
                cookies[INDEX_SESSION_COOKIE].Value = idSession;
            else
                cookies.Add(new HttpCookie(INDEX_SESSION_COOKIE, idSession));
        }

        public T récupérerLaSession()
        {
            HttpContext contexte = HttpContext.Current;
            if (contexte != null && unDesCookiesContientUneSession(contexte.Request.Cookies))
                return récupérerLaSessionDuContexteHTTP(contexte);
            return default(T);
        }

        private T récupérerLaSessionDuContexteHTTP(HttpContext contexte)
        {
            string idSession = string.Empty;
            HttpCookie cookie = contexte.Request.Cookies[INDEX_SESSION_COOKIE];
            if (cookie != null)
                idSession = cookie.Value;
            if (aLaSession(idSession))
            {
                T sessionRécupérée = _sessions[idSession];
                if (estUneSessionValide(sessionRécupérée))
                    return sessionRécupérée;
                supprimerLaSession(idSession);
            }
            return default(T);
        }


        private bool unDesCookiesContientUneSession(HttpCookieCollection cookies)
        {
            return cookies.AllKeys.Any(clé => clé == INDEX_SESSION_COOKIE);
        }

        private bool aLaSession(string idSession)
        {
            return _sessions.ContainsKey(idSession);
        }

        private bool estUneSessionValide(T session)
        {
            return session.DateConnexion.AddHours(DÉLAI_SESSION) > DateTime.Now;
        }

        private void supprimerLaSession(string idSession)
        {
            _sessions.Remove(idSession);
        }
        
    }
}
