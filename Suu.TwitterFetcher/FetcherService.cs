using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suu.TwitterFetcher
{
    class FetcherService
    {
        public void Start()
        {
        }
        public void Stop()
        {
            LogService("Service Stoped");
        }

        private void LogService(string content)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Suu.TwitterFetcher.Logfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
