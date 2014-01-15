using Nancy;

namespace Yeasca.Web
{
    public class ConnexionModule: NancyModule
    {
        public ConnexionModule()
        {
            Get["/"] = _ =>
                {
                    return View["Connexion.html"];
                };
        }
    }
}