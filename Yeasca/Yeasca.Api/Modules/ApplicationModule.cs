using Nancy;

namespace Yeasca.Web
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule()
        {
            Get["/Application"] = _ =>
            {
                return View["Application.html"];
            };
        }
    }
}