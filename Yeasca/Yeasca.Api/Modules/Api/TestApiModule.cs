using Nancy;

namespace Yeasca.Web.Api
{
    public class TestApiModule : NancyModule
    {
        public TestApiModule()
        {
            Get["/Api/Test"] = _ =>
            {
                return View["/Api/TestApi.html"];
            };
        }
    }
}