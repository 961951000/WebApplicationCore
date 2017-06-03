using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplicationCore.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private IHostingEnvironment hostingEnv = null;

        public FileUploadController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        public IActionResult UploadFiles()

        {

            return View();

        }
        [HttpPost]
        public IActionResult UploadFiles(IList<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var filepath = Path.Combine(hostingEnv.WebRootPath, "files");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filename = Path.Combine(filepath, filename);
                size += file.Length;
                using (var fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            ViewBag.Message = $"{files.Count} 文件 /{ size}字节上传成功!";
            return View();
        }
    }
}