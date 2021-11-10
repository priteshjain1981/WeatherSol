using System;
using System.Net.Http;
using Weather_Shared;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;


namespace WeatherNews
{
    class Program
    {        
        static void Main(string[] args)
        {
            Console.WriteLine("Activated Weather Service");

            try
            {
                string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
                if (!Directory.Exists(sourcePath))
                {
                    DirectoryInfo dir = new DirectoryInfo(sourcePath);
                    dir.Create();
                }                        
                 

                string targetPath = ConfigurationManager.AppSettings["TargetPath"];
                if (!Directory.Exists(targetPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(targetPath);
                    dir.Create();
                }

                FileParser parser = new FileParser();
                parser.ActivateListener(sourcePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            Console.ReadLine();
        }
                
    }
}

  
