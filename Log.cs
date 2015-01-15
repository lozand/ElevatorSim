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
            IsWriting = false;
            Initialize();   
        }

        string fileName = "D:\\Log.txt";

        public void WriteToFile(string text, string level)
        {
            bool successfulWrite = false;
            while (!successfulWrite)
            {
                if (!IsWriting)
                {
                    IsWriting = true;
                    Set(level);
                    Console.WriteLine(text);
                    Reset();
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.Write(text);
                        sw.WriteLine("");
                    }
                    IsWriting = false;
                    successfulWrite = true;
                }
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

        private bool IsWriting { get; set; }

        private void Set(string state = "")
        {
            switch (state.ToLower())
            {
                case "g":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "r":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "i":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "l":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        private void Reset()
        {
            Console.ResetColor();
        }
    }
}
