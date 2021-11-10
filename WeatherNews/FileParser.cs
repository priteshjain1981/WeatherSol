using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace WeatherNews
{
    public class FileParser
    {
        public  void ActivateListener(string incomingFilepath)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = incomingFilepath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.txt*";
            watcher.Changed += new FileSystemEventHandler(OnFileReceived);
            watcher.EnableRaisingEvents = true;

            try
            {
                string outputFolder = ConfigurationManager.AppSettings["TargetPath"];
                string outputFile = outputFolder + "City" + DateTime.Now.Date.ToShortDateString() + ".txt";
                if (!File.Exists(outputFile))
                {
                    using (FileStream fs = File.Create(outputFile)) { }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file creation failed: {0}", e.ToString());
            }
        }

        private void OnFileReceived(object sender, FileSystemEventArgs e)
        {
            try
            {
                ParseFile(e.FullPath);              
            }
            catch(System.IO.FileNotFoundException)
            {
                //do nothing if the file handle is still not ready
            }
            catch(Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
        }

        private void ParseFile(string file)
        {
            string folder = ConfigurationManager.AppSettings["TargetPath"];
            string outputFile = folder + "City" + DateTime.Now.Date.ToShortDateString() + ".txt";
            try
            {
               
                StringBuilder citiesWeather = new StringBuilder();
                using (StreamReader sr = new StreamReader(file))
                {
                    string line; 
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] cityId = line.Split('=');
                        Console.WriteLine(line);
                        GetWeather getWeather = new GetWeather();
                        var cityWeather = getWeather.GetWeatherInformation(cityId[0],null);
                        citiesWeather.AppendLine(cityWeather.Result);
                    }                   
                }
                using (StreamWriter targetFileWriter = new StreamWriter(outputFile, append: true))
                {
                    targetFileWriter.WriteLine(citiesWeather);
                }
                PurgeSource(file);
            }
            catch (System.IO.FileNotFoundException)
            {
                //do nothing if the file handle is still not ready
            }
            catch (Exception e)
            {
                Console.WriteLine("The file operation failed: {0}", e.ToString());
            }

            
        
        }

        private void PurgeSource(string parsedFile)
        {          
            try
            {
                // delete source 
                if (File.Exists(parsedFile))
                    File.Delete(parsedFile);                
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
