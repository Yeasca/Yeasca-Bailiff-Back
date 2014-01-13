using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Outils.Sessions
{
    [TestClass]
    public class TestSessions
    {
        private readonly SessionMock _session = new SessionMock() {Message = "poulou"};

        [TestCleanup]
        public void supprimerLeContexte()
        {
            HttpContext.Current = null;
        }

        [TestMethod]
        public void TestSessions_peutSauvegarderUneSession()
        {
            HttpContext.Current = ConstantesTest.CONTEXTE_HTTP;
            Sessions<SessionMock> sessions = new Sessions<SessionMock>();
            SessionMock nouvelleSession = _session;
            string idSession = sessions.ajouterUneSession(nouvelleSession);

            Assert.IsNotNull(HttpContext.Current);

            HttpCookie cookie = HttpContext.Current.Response.Cookies[Sessions<SessionMock>.INDEX_SESSION_COOKIE];
            Assert.IsNotNull(cookie);
            Assert.AreEqual(idSession, cookie.Value);
        }

        [TestMethod]
        public void TestSessions_peutRécupérerUneSession()
        {
            HttpContext.Current = ConstantesTest.CONTEXTE_HTTP;
            Sessions<SessionMock> sessions = new Sessions<SessionMock>();
            SessionMock nouvelleSession = _session;
            string idSession = sessions.ajouterUneSession(nouvelleSession);
            mettreLaSessionDansLaRequete(idSession);
            SessionMock sessionRécupérée = sessions.récupérerLaSession();

            Assert.IsNotNull(sessionRécupérée);
            Assert.AreEqual(nouvelleSession.Message, sessionRécupérée.Message);
        }

        private void mettreLaSessionDansLaRequete(string idSession)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            cookies.Add(new HttpCookie(Sessions<SessionMock>.INDEX_SESSION_COOKIE, idSession));
        }
    }

    public class SessionMock : ISession
    {

        public Guid ID { get; set; }
        public DateTime DateConnexion { get; set; }
        public string Message { get; set; }
    }
}
