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
                    var imageName = $"{user.Id.ToString()}.{extension}";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    client.DownloadFileAsync(new Uri(user.profile_image_url), $"{filePath}{ imageName}");
                }
            }
                
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
             Console.WriteLine("File downloaded");
        }
    }
}
