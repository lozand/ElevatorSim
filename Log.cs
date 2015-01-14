using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ElevatorSim
{
    public class Log
    {
        public Log()
        {
            Initialize();   
        }

        string fileName = "D:\\Log.txt";

        public void WriteToFile(string text)
        {
            Console.WriteLine(text);
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.Write(text);
                sw.WriteLine("");
            }
        }

        private void Initialize()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (FileStream fs = File.Create(fileName))
            {
            }
        }
    }
}
