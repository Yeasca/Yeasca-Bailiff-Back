using Nancy;

namespace Yeasca.Web
{
    public class ConstatsModule: NancyModule
    {
        public ConstatsModule()
        {
            Get["/Constat"] = _ =>
                {
                    return View["Constats.html"];
                };
        }
    }
}