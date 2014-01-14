using Nancy;

namespace Yeasca.Api
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/"] = _ =>
            {
                return View["Index.html"];
            };
        }
    }
}