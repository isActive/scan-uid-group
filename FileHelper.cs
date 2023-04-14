using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanUIDMemberGroup
{
    public class FileHelper
    {
        public static void SaveSuccess(string data,string idGroup)
        {
            string pathSave = AppDomain.CurrentDomain.BaseDirectory + $"\\success_{idGroup}.txt";
            var filetoappend = new FileInfo(pathSave);
            using (var sw = filetoappend.AppendText())
            {
                sw.WriteLine(data);
            }
        }
    }
}
