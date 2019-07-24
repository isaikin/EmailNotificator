using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.EmailNotoficator.WebPL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        [ActionName("UploadFile")]
        public ActionResult UploadFilePost()
        {
            var fileRequest = this.Request.Files["UploadFile"];

            using (MemoryStream streamFile = new MemoryStream())
            {
                fileRequest.InputStream.CopyTo(streamFile);


            }
                return View();
        }
    }
}