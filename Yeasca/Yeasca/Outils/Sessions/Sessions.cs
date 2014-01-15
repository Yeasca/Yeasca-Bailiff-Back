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
            Guid nouvelID = Guid.NewGuid();
            session.ID = nouvelID;
            session.DateConnexion = DateTime.Now;
            string nouvelIDEnChaine = nouvelID.ToString();
            _sessions.Add(nouvelIDEnChaine, session);
            modifierLeCookieAvecLIDSession(nouvelIDEnChaine, HttpContext.Current.Request.Cookies);
            modifierLeCookieAvecLIDSession(nouvelIDEnChaine, HttpContext.Current.Response.Cookies);
            return nouvelIDEnChaine;
        }

        private void modifierLeCookieAvecLIDSession(string idSession, HttpCookieCollection cookies)
        {
            if (unDesCookiesContientUneSession(cookies))
                cookies[INDEX_SESSION_COOKIE].Value = idSession;
            else
                cookies.Add(new HttpCookie(INDEX_SESSION_COOKIE, idSession));
        }

        public T récupérerLaSession()
        {
            supprimerLesSessionsExpirées();
            HttpContext contexte = HttpContext.Current;
            if (contexte != null && unDesCookiesContientUneSession(contexte.Request.Cookies))
                return récupérerLaSessionDuContexteHTTP(contexte);
            return default(T);
        }

        private void supprimerLesSessionsExpirées()
        {
            List<string> clésDesSessionsASupprimer = new List<string>();
            foreach (KeyValuePair<string, T> paireCléValeur in _sessions)
                récupérerUneCléDeSessionASupprimer(paireCléValeur, clésDesSessionsASupprimer);
            foreach (string clé in clésDesSessionsASupprimer)
                supprimerLaSession(clé);
        }

        private static void récupérerUneCléDeSessionASupprimer(KeyValuePair<string, T> paireCléValeur, List<string> clésDesSessionsASupprimer)
        {
            if (paireCléValeur.Value.DateConnexion > DateTime.Now.AddHours(DÉLAI_SESSION))
                clésDesSessionsASupprimer.Add(paireCléValeur.Key);
        }

        private T récupérerLaSessionDuContexteHTTP(HttpContext contexte)
        {
            string idSession = string.Empty;
            HttpCookie cookie = récupérerLeCookie(contexte);
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

        private HttpCookie récupérerLeCookie(HttpContext contexte)
        {
            HttpCookie cookie = contexte.Request.Cookies[INDEX_SESSION_COOKIE];
            if (cookie != null)
                return cookie;
            return null;
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

        public void déconnecter()
        {
            HttpContext contexte = HttpContext.Current;
            HttpCookie cookie = récupérerLeCookie(contexte);
            if(cookie != null && aLaSession(cookie.Value))
                supprimerLaSession(cookie.Value);
        }
        
    }
}
