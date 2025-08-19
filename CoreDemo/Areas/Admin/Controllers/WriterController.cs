using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Concrete;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Bibliography;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        //Context c = new Context();
        //WriterManager wm = new WriterManager(new EfWriterRepository());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WriterList()
        {
            //var writerss = GetWriter();
            //var jsonWriters = JsonConvert.SerializeObject(writerss);
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }
        public IActionResult GetWriterByID(int writerid)
        {
            //var writerss = GetWriter();
            //var findWriter = writerss.FirstOrDefault(x => x.Id == writerid);
            var findWriter = writers.FirstOrDefault(x => x.Id == writerid);
            var jsonWriters= JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }
        [HttpPost]
        public IActionResult AddWriter(WriterClass w)
        {
            //var writerss = GetWriter();

            //Writer writer = new Writer
            //{
            //    WriterName = w.Name,
            //    WriterAbout = w.About,
            //    WriterMail = w.Mail,
            //    WriterImage = w.Image,
            //    WriterPassword=w.password,
            //    WriterStatus = w.status,


            //};
            //wm.TAdd(writer);
            //writerss.Add(w);
            writers.Add(w);
            var jsonWriters = JsonConvert.SerializeObject(w);
            return Json(jsonWriters);  
        }
        public IActionResult DeleteWriter(int id)
        {
            //var writerss = GetWriter();


            //var writer = writerss.FirstOrDefault(x=>x.Id==id);
            //var writervalue = wm.TGetById(id);
            //wm.TDelete(writervalue);
            //writerss.Remove(writer);
            var writer = writers.FirstOrDefault(x=>x.Id==id);

            
            writers.Remove(writer);
            return Json(writer);
        }

        public IActionResult UpdateWriter(WriterClass w)
        {
            //var writerss = GetWriter();
            //var writer=writerss.FirstOrDefault(x=>x.Id==w.Id);
            var writer=writers.FirstOrDefault(x=>x.Id==w.Id);
            writer.Name=w.Name;
            var jsonWriter=JsonConvert.SerializeObject(w);
            return Json(jsonWriter);

        }
        //public List<WriterClass> GetWriter()
        //{

        //        List<WriterClass> writerss = new List<WriterClass>();

        //        foreach (var writerr in c.Writers.ToList())
        //        {


        //            writerss.Add(new WriterClass
        //            {
        //                Id = writerr.WriterID,
        //                Name = writerr.WriterName,
        //                About=writerr.WriterAbout,
        //                Mail=writerr.WriterMail,
        //                Image=writerr.WriterImage,
        //                password=writerr.WriterPassword,
        //                status=writerr.WriterStatus
        //            });
        //        }

        //        return writerss;

        //}


        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id=1,
                Name="Ayşe"
            },
             new WriterClass
            {
                Id=2,
                Name="Ahmet"
            },
              new WriterClass
            {
                Id=3,
                Name="Buse"
            }

        };
    }
}
