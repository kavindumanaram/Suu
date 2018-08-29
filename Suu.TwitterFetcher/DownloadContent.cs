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
using System.Configuration;

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
                    if (user.is_ready == 0)
                    {
                        WebClient client = new WebClient();
                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                        var filePath = AppDomain.CurrentDomain.BaseDirectory;
                        string projectPath = ConfigurationManager.AppSettings["filePath"];

                        if (!string.IsNullOrEmpty(projectPath))
                        {
                            filePath = projectPath + "\\Suu.FrontEnd\\assets\\Suu.Temp\\ProfilePic\\";
                        }

                        var extension = Path.GetExtension(user.profile_image_url);
                        var imageName = $"{user.Id.ToString()}{extension}";
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        try
                        {
                            client.DownloadFile(new Uri(user.profile_image_url), $"{filePath}{ imageName}");
                            user.is_ready = 1;
                            SuuContext.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    
                     

                    //var dbx = new DropboxClient("bwUjLwfs0zAAAAAAAAAAGmL_efee1PRAMvTqewDyfZeu4eV5iLa677oEzwheG49y");
                    //var t = Task.Run(() => Upload(dbx, "/Suu.TweeterFetcher", imageName, $"{filePath}{ imageName}"));
                    //t.Wait();

                    //var v = Task.Run(() => Upload(dbx, "/Suu.TweeterFetcher", imageName, $"{filePath}{ imageName}"));
                    //v.Wait();
                    ////////////////////////////
                    ////client.
                    ////var result =  dbx.Sharing.CreateSharedLinkWithSettingsAsync("/Suu.TweeterFetcher");
                    ////var url = result.Result.Url;
                    ////Console.WriteLine(url.ToString());

                    //var tempPath = dbx.Files.GetTemporaryLinkAsync($"/Suu.TweeterFetcher{filePath}{ imageName}");
                    //Console.WriteLine(tempPath);

                 //   var x = DownloadThumbnail(string path, ThumbnailFormat format, ThumbnailSize size);
                }
            }

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("File downloaded");
        }


        //async Task Upload(DropboxClient dbx, string folder, string file, string content)
        //{
        //    using (var mem = new MemoryStream(File.ReadAllBytes(content)))
        //    {
        //        var updated = await dbx.Files.UploadAsync(
        //            folder + "/" + file,
        //            WriteMode.Overwrite.Instance,
        //            body: mem);
        //        Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
        //    }
        //}


    }
}
