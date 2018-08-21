using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Suu.FrontEnd.Models;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Suu.TwitterFetcher
{
    class DownloadContent : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (SuuEntities SuuContext = new SuuEntities())
            {
                IEnumerable<User> userList = SuuContext.Users.ToList();

                foreach (var user in userList)
                {

                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Inserts\\";
                    var extension = Path.GetExtension(user.profile_image_url);
                    var imageName = $"{user.Id.ToString()}{extension}";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                     client.DownloadFile(new Uri(user.profile_image_url), $"{filePath}{ imageName}");
                    var dbx = new DropboxClient("bwUjLwfs0zAAAAAAAAAAGmL_efee1PRAMvTqewDyfZeu4eV5iLa677oEzwheG49y");
                    var t = Task.Run(() => Upload(dbx, "/Suu.TweeterFetcher", imageName, $"{filePath}{ imageName}"));
                    t.Wait();

                }
            }

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("File downloaded");
        }


        async Task Upload(DropboxClient dbx, string folder, string file, string content)
        {
            using (var mem = new MemoryStream(File.ReadAllBytes(content)))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
            }
        }
    }
}
