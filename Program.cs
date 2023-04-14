using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScanUIDMemberGroup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "SCAN UID MEMBERS GROUP BY MINH HIEN";
            Crawl();

            Console.ReadLine();
        }
        static void Crawl()
        {
            REBACK:
            int checkMem = 0;
            string cookie = "";
            do
            {
                Console.Write("INPUT COOKIE: ");
                cookie = Console.ReadLine();

            } while (!cookie.Contains("c_user") || string.IsNullOrEmpty(cookie));
            Console.Clear();
            Console.Write("INPUT UID GROUP: ");
            var idGroup = Console.ReadLine();
            Console.Write("INPUT AMOUNT: ");
            var amount = Console.ReadLine();
            Request request = new Request(cookie);
            string resGroup = request.RequestGet($"https://mbasic.facebook.com/browse/group/members/?id={idGroup}&start=0&listType=list_nonfriend_nonadmin");
            if(Regex.Match(resGroup, "<title>(.*?)</title>").Groups[1].Value.Contains("Đăng nhập"))
            {
                Console.Clear();
                Console.WriteLine("ĐĂNG NHẬP THẤT BẠI! ADD LẠI COOKIE");
                goto REBACK;
            }
            Console.Clear();
        ReGet:
            MatchCollection matches = Regex.Matches(resGroup, "id=\"member_(.*?)\"");
            string iMore = Regex.Match(resGroup, "id=\"m_more_item\"><a href=\"(.*?)\"").Groups[1].Value.Replace("&amp;","&");
            Console.Title = $"SCAN UID MEMBERS GROUP BY MINH HIEN - RUNNING GROUP {idGroup}";
            foreach (Match id in matches)
            {
                string pathID = Regex.Match(id.Value.ToString(), "member_(.*?)\"").Groups[1].Value;
                if (checkMem == Int32.Parse(amount))
                {
                    Console.WriteLine("Crawl DONE");
                    return;
                } // Check Amount
                Console.WriteLine($"[{checkMem++}] - {pathID}");
                FileHelper.SaveSuccess(pathID, idGroup);
            }
            resGroup = request.RequestGet($"https://mbasic.facebook.com{iMore}");
            goto ReGet;
        }
    }
}
