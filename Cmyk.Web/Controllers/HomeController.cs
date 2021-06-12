using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Cmyk.Web.Helpers;

namespace Cmyk.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(HttpPostedFileBase file)
        {
            var isJpg = IsJpg(file);

            var imageResult = new ImageResultView()
            {
                Filename = file.FileName,
                IsJpg = isJpg
            };

            if (!imageResult.IsJpg) return Json(imageResult);

            using (var image = Image.FromStream(file.InputStream))
                imageResult.IsCmyk = image.IsCmyk();

            return Json(imageResult);
        }

        private bool IsJpg(HttpPostedFileBase file)
        {
            var split = file.FileName.Split('.');
            var ext = split[split.Length - 1].ToLower();

            return ext == "jpg" || ext == "jpeg";
        }
	}

    public class ImageResultView
    {
        public string Filename { get; set; }
        public bool IsJpg { get; set; }
        public bool IsCmyk { get; set; }
    }
}