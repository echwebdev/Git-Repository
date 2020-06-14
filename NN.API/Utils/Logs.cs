using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Logs
    {
        public static void Append(string errorMsg)
        {
            try
            {
                Log log = new Log();
                string path = Path.Combine(Path.GetDirectoryName(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath), "logs");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                System.IO.StreamWriter sw = new StreamWriter(path + @"\" + log.sErrorTime + ".txt", true);
                lock (sw as object)
                {
                    sw.WriteLine(log.sLogFormat);
                    sw.WriteLine(errorMsg);
                    sw.WriteLine();
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public static void Append()
        {
            throw new NotImplementedException();
        }
    }

    class Log
    {
        public string sLogFormat;
        public string sErrorTime;

        //Constructor
        public Log()
        {
            //sLogFormat used to create log files format : dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //This variable used to create log filename format, for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + "-" + sMonth + "-" + sDay;
        }
    }
}
