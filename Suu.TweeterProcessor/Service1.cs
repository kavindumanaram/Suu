using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Suu.FrontEnd.Models;
namespace Suu.TweeterProcessor
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timeDelay;
        int count;
        private Timer time1 = null;

        public Service1()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }

        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            string process = DateTime.Now.ToString() + "Timer Tick " + count;
            LogService(process);
            count++;

            try
            {
                using (SuuEntities SuuContext = new SuuEntities()) {
                    var x = new TwitterMessage();
                    x.Text = DateTime.Now.ToString();
                    SuuContext.TwitterMessages.Add(x);
                    SuuContext.SaveChanges();
                }
                //using (SuuDatabaseEntities Suu = new SuuDatabaseEntities())
                //{
                //    var x = new TwitterMessage();
                //    x.CreatedAt = DateTime.Now.ToString();
                //    Suu.TwitterMessages.Add(x);
                //    Suu.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                LogService("=========Exception==========" + ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            timeDelay.Interval = 30000; //30 seconds
            LogService("Service is Started....");
            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }

        private void LogService(string content)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Suu.Logfile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
