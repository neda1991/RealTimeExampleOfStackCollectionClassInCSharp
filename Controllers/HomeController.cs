using Microsoft.AspNetCore.Mvc;

namespace RealTimeExampleOfStackCollectionClassInCSharp.Controllers
{
    public class HomeController : Controller
    {
        Stack<string> stackUrls = new Stack<string>();

        public HomeController()
        {

        }
        public IActionResult Index(Dictionary<string, string> data)
        {

            PushUrlsToStack(data, "https://localhost:44321/home/index");
            return View();
        }

        public IActionResult Index1(Dictionary<string, string> data)
        {

            PushUrlsToStack(data, "https://localhost:44321/home/index1");
            return View();
        }

        public IActionResult Index2(Dictionary<string, string> data)
        {

            PushUrlsToStack(data, "https://localhost:44321/home/index2");

            return View();
        }

        public IActionResult Index3(Dictionary<string, string> data)
        {

            PushUrlsToStack(data, "https://localhost:44321/home/index3");
            return View();
        }

        private void PushUrlsToStack(Dictionary<string, string> data, string newUrl)
        {

            foreach (var url in data.Reverse())
            {
                stackUrls.Push(url.Value);
            }
            stackUrls.Push(newUrl);

            TempData["stackUrls"] = stackUrls;

        }


        public IActionResult BackToPreviousPage(List<string> urls)
        {
            for (int i = urls.Count - 1; i >= 0; i--)
            {
                stackUrls.Push(urls[i]);
            }

            string currentUrl = stackUrls.Pop();
            string backToThisUrl;
            if (stackUrls.Count != 0)
            {
                backToThisUrl = stackUrls.Pop();
            }
            else
            {
                TempData["ThereIsNoPreviousLink"] = "This is the first page you visited.Do not" +
                " press back button";
                backToThisUrl = currentUrl;

            }

            string actionName = backToThisUrl.Split('/').Last();

            Dictionary<string, string> stackUrlsDictioanry = new Dictionary<string, string>();
            var counter = stackUrls.Count;
            for (int i = 1; i <= counter; i++)
            {
                stackUrlsDictioanry.Add(i.ToString(), stackUrls.Pop());
            }

            return RedirectToAction($"{actionName}", stackUrlsDictioanry);

        }
    }
}