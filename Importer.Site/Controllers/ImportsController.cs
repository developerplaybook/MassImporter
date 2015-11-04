using System;
using System.IO;
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
                if (upload != null && upload.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(upload.FileName));
                        upload.SaveAs(path);
                    }
                    catch (Exception)
                    {
                        throw  new ApplicationException("Something Wrong Happened");
                        
                    }
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
