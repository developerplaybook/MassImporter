using System.Web;
using System.Web.Mvc;
using Importer.Site.Models;

namespace Importer.Site.Controllers
{
    public class ImportsController : Controller
    {
        
        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        // POST: Imports/Create
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var import = new FilePath
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),

                    };
                }

                return RedirectToAction("Import");
            }
            catch
            {
                return View();
            }
        }

       
    }
}
