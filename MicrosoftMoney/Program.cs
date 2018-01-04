using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MicrosoftMoney
{
    class Program
    {
        private static int count;
        private static List<char> myChar;
        private static List<string> xml;
        private static List<string> xmlSetting;
        private static string moneyFile;
        private static byte[] fileToByte;
        private static byte[] fileToByte2;

        public int Count { get => count; set => count = value; }
        public List<char> MyChar { get => myChar; set => myChar = value; }
        public List<string> Xml { get => xml; set => xml = value; }
        public List<string> XmlSetting { get => xmlSetting; set => xmlSetting = value; }
        public string MoneyFile { get => moneyFile; set => moneyFile = value; }
        public byte[] FileToByte { get => fileToByte; set => fileToByte = value; }
        public byte[] FileToByte2 { get => fileToByte2; set => fileToByte2 = value; }

        static void Main(string[] args)
        {
            
            string moneyFile = @"c:\users\frost\source\repos\MicrosoftMoney\MicrosoftMoney\usaa_money.ofx";
            byte[] fileToByte = File.ReadAllBytes(moneyFile);
            List<string> xml = new List<string>();
            List<string> xmlSetting = new List<string>();
            var mychar = new List<char>();
            byte[] fileToByte2 = new byte[500];
            int count = 0;
            foreach (byte b in fileToByte)
            {
                try
                {
                    if (b < 127)
                    {
                        fileToByte2.SetValue(b, count);
                        count++;
                    }
                }
                catch (IndexOutOfRangeException)
                {

                    break;
                }
                
            }
            Array.Resize(ref fileToByte2, count);

            count = 0; int start = 0, stop = 0;
            foreach (byte b in fileToByte2)
            {
                if (b == 60)
                {
                    char[] ch = new char[(count - start )];
                    for (int i = 0; i < (count - start ); i++)
                    {
                        ch[i] = (char)fileToByte2[start + i];
                    }
                    string s = new string(ch);
                    xmlSetting.Add(s);
                    start = count;
                }

                if (b == 62)
                {
                    stop = count;
                    char[] ch = new char[(stop - start + 1)];
                    for (int i = 0; i < (stop - start + 1); i++)
                    {
                        ch[i] = (char)fileToByte2[start + i];
                    }
                    string mystring = new string(ch);
                    xml.Add(mystring);
                    start = stop + 1;
                }
                count++;
                
            }
            foreach (string st in xml)
            {
                Console.WriteLine(st);
            }
            foreach (string st in xmlSetting)
            {
                Console.WriteLine(st);
            }
        }    
    }
}
