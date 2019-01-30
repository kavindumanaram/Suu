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
using System.Configuration;
using Newtonsoft.Json;
using Suu.TwitterFetcher.Dto;
//using GoogleMaps.LocationServices;

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
                        string SuuProPicPath = ConfigurationManager.AppSettings["filePath"];

                        var extension = Path.GetExtension(user.profile_image_url);
                        var imageName = $"{user.Id.ToString()}{extension}";
                        if (!Directory.Exists(SuuProPicPath))
                        {
                            Directory.CreateDirectory(SuuProPicPath);
                        }
                        try
                        {
                            client.DownloadFile(new Uri(user.profile_image_url), $"{SuuProPicPath}{ imageName}");
                            user.is_ready = 1;
							//GetCoordinatesOfUserLocation(user);
							var currentUserlocation = user.location.ToLower();
							if (currentUserlocation.Any())
							{
								var existingLoactionList = SuuContext.UserLocationCounts.Select(g => g.user_location);

								var latt = 0.00;
								var lont = 0.00;
								var locationText = string.Empty;
								HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://open.mapquestapi.com/geocoding/v1/address?key=WBA9ECUGd5XzoEeYlTPMKOivfEEMfTyk&location=" + currentUserlocation);
								request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

								using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
								using (Stream stream = response.GetResponseStream())
								using (StreamReader reader = new StreamReader(stream))
								{
									var x = reader.ReadToEnd();
									//var y =  serializer.DeserializeObject(reader.ReadToEnd());
									var GeoLocationResponseResponse = JsonConvert.DeserializeObject<GeocodingByAddressDto>(x);
									latt = GeoLocationResponseResponse.results.FirstOrDefault().locations.FirstOrDefault().displayLatLng.lat;
									lont = GeoLocationResponseResponse.results.FirstOrDefault().locations.FirstOrDefault().displayLatLng.lng;
									locationText = GeoLocationResponseResponse.results.FirstOrDefault().locations.FirstOrDefault().adminArea5;
								}

								if (!string.IsNullOrEmpty(locationText))
								{
									if (!existingLoactionList.Contains(locationText))
									{

										//var locationCount = new FrontEnd.Models.UserLocationCount()
										//{
										//	user_location = currentUserlocation,
										//	count = 1,
										//	lon = lont.ToString(),
										//	lat = latt.ToString()
										//};

										//try
										//{


										var locationCount = new FrontEnd.Models.UserLocationCount()
										{
											user_location = locationText,
											count = 1,
											lon = lont.ToString(),
											lat = latt.ToString()
										};
										//}
										//catch (Exception ex)
										//{
										//	Console.WriteLine("Error while get coordinates" + ex.Message);
										//}


										SuuContext.UserLocationCounts.Add(locationCount);
									}
									else
									{
										{
											var exsitingLocationCountText = SuuContext.UserLocationCounts.Where(d => d.user_location == locationText).FirstOrDefault();
											var exsitingLocationCountTextOccurence = exsitingLocationCountText.count;
											var exsitingLocationCountTextLatestOccurence = exsitingLocationCountTextOccurence + 1;
											exsitingLocationCountText.count = exsitingLocationCountTextLatestOccurence;
										}
									}
								}
								
							}




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

			Console.WriteLine("finish user image and location process cycle .......");
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
