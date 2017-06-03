using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplicationCore.Controllers
{
    public class FileDownLoadController : Controller
    {
        private IHostingEnvironment hostingEnv = null;

        public FileDownLoadController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }
        // GET: Download
        public ActionResult Index()
        {
            var filepath = Path.Combine(hostingEnv.WebRootPath, "files");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            ViewData["files"] = GetAll(new DirectoryInfo(filepath)).ToArray();
            return View();
        }

        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <param name="fileName">�ļ�����</param>
        public FileStreamResult DownFile(string filePath, string fileName)
        {
            return File(new FileStream(Path.Combine(filePath, fileName), FileMode.Open), "application/octet-stream", fileName);
        }

        /// <summary>
        /// �����ļ����е��ļ�
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private IEnumerable<FileInfo> GetAll(DirectoryInfo dir)
        {
            var files = new List<FileInfo>();
            if (IsSystemHidden(dir)) return files;
            files.AddRange(dir.GetFiles());
            var allDir = dir.GetDirectories();
            foreach (var d in allDir)
            {
                files.AddRange(GetAll(d));
            }
            return files;
        }
        /// <summary>
        /// �����ļ�����
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private bool IsSystemHidden(DirectoryInfo dir)
        {
            if (dir.Parent == null)
            {
                return false;
            }
            var attributes = dir.Attributes.ToString();
            return attributes.IndexOf("Hidden", StringComparison.Ordinal) > -1 && attributes.IndexOf("System", StringComparison.Ordinal) > -1;
        }
    }
}